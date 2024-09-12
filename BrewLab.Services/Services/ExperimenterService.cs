using BrewLab.Common.DTOs;
using BrewLab.Common.JWT;
using BrewLab.Models;
using BrewLab.Models.Models;
using BrewLab.Repository.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text;
using BrewLab.Common.Configuration;
using System.Text.Json;

namespace BrewLab.Services;
public class ExperimenterService(
    BrewLabContext context,
    PasswordHasher<Experimenter> hasher,
    UserManager<Experimenter> userManager,
    IHttpClientFactory _httpClientFactory) : Repository<Experimenter>(context)
{
    private readonly PasswordHasher<Experimenter> _hasher = hasher;
    private readonly UserManager<Experimenter> _userManager = userManager;
    private readonly IHttpClientFactory _httpClientFactory = _httpClientFactory;
    private readonly EmailConfig Email = Configs.Email;

    public async Task<ResultDTO.Result> DeleteAccountAsync(int experimenterId, string password)
    {
        var experimenter = await FindSingle(e => e.Id == experimenterId);

        if (experimenter is null || experimenter.PasswordHash is null) return ResultDTO.Result.ExperimenterDoesNotExist;

        var result = _hasher.VerifyHashedPassword(experimenter, experimenter.PasswordHash, password);

        if (result == PasswordVerificationResult.Failed) return ResultDTO.Result.IncorrectPassword;

        await Delete(experimenter);

        return ResultDTO.Result.Succeeded;
    }

    public async Task<ResultDTO.Result> AttemptUpdate(ExperimenterDTO.Account account)
    {
        var experimenter = await FindSingle(e => e.Id == account.Id);

        if (experimenter is null || experimenter.PasswordHash is null) return ResultDTO.Result.ExperimenterDoesNotExist;

        var result = _hasher.VerifyHashedPassword(experimenter, experimenter.PasswordHash, account.CurrentPassword);

        if (result == PasswordVerificationResult.Failed) return ResultDTO.Result.IncorrectPassword;

        experimenter.Email = account.Email;
        experimenter.Name = account.Name;
        if (!string.IsNullOrWhiteSpace(account.NewPassword) || !string.IsNullOrWhiteSpace(account.NewPasswordRepeat))
        {
            if (ExperimenterDTO.ValidateRepeatPassword(account.NewPassword, account.NewPasswordRepeat).Any()) return ResultDTO.Result.RepeatPasswordDoesNotMatch;

            await _userManager.ChangePasswordAsync(experimenter, account.CurrentPassword, account.NewPassword);
        }

        await Update(experimenter);

        return ResultDTO.Result.Succeeded;
    }

    public async Task<ExperimenterDTO.Account?> GetById(int id)
    {
        var experimenter = await Get<Experimenter>()
            .Where(e => e.Id == id)
            .Select(e => new ExperimenterDTO.Account()
            {
                Id = e.Id,
                UserName = e.UserName!,
                Name = e.Name,
                Email = e.Email!,
                CurrentPassword = "",
            })
            .FirstOrDefaultAsync();

        return experimenter;
    }

    public async Task<ResultDTO.Auth> Login(ExperimenterDTO.Login login)
    {
        var experimenter = await FindSingle(e => e.UserName == login.UserName);

        if (experimenter is null || experimenter.PasswordHash is null || experimenter.UserName is null)
        {
            return ResultDTO.Auth.IncorrectLoginOrPassword;
        }

        var result = _hasher.VerifyHashedPassword(experimenter, experimenter.PasswordHash, login.Password);

        if (result == PasswordVerificationResult.Success)
        {
            var nameAndId = new ExperimenterDTO.NameAndId()
            { Id = experimenter.Id, UserName = experimenter.UserName };
            return new ResultDTO.Auth
            {
                Success = true,
                Token = Token.GenerateToken(nameAndId),
                Experimenter = nameAndId
            };
        }
        else
            return ResultDTO.Auth.IncorrectLoginOrPassword;
    }

    public async Task<ExperimenterDTO.NameAndId?> Validate(string token)
    {
        var tokenExperimenter = await Token.ReadToken(token);

        if (tokenExperimenter is null) return null;

        var dbExperimenter = await FindSingle(e => e.Id == tokenExperimenter.Id);

        if (dbExperimenter is null) return null;

        return tokenExperimenter;
    }

    public async Task<ResultDTO.Auth> Register(ExperimenterDTO.Register register)
    {
        var experimenterModel = new Experimenter()
        {
            Name = register.Name,
            Email = register.Email,
            UserName = register.UserName,
        };

        var result = await _userManager.CreateAsync(experimenterModel, register.Password);

        var nameAndId = new ExperimenterDTO.NameAndId()
        { Id = experimenterModel.Id, UserName = experimenterModel.UserName };

        var returnVal = new ResultDTO.Auth
        {
            Success = result.Succeeded,
            Errors = result.Errors.Select(e => e.Description),
            Token = Token.GenerateToken(nameAndId),
            Experimenter = nameAndId
        };

        if (returnVal.Errors.Any(e => e.Contains("already taken")))
        {
            returnVal.Errors = ["Nome de usuário já existe."];
        }

        return returnVal;
    }

    public async Task<ResultDTO.Result> RedefinePasswordAsync(string newPassword, string newPasswordRepeat, int experimenterId)
    {
        var experimenter = await FindSingle(e => e.Id == experimenterId);

        if (experimenter is null) return ResultDTO.Result.ExperimenterDoesNotExist;

        if (ExperimenterDTO.ValidateRepeatPassword(newPassword, newPasswordRepeat).Any()) return ResultDTO.Result.RepeatPasswordDoesNotMatch;

        var token = await _userManager.GeneratePasswordResetTokenAsync(experimenter);

        var result = await _userManager.ResetPasswordAsync(experimenter, token, newPassword);

        if (!result.Succeeded) return ResultDTO.Result.UnknownError;

        return ResultDTO.Result.Succeeded;
    }

    public async Task<ResultDTO.Result> SendPasswordRecoveryEmailAsync(string username, string baseUrl)
    {
        var experimenter = await FindSingle(e => e.UserName == username);

        if (experimenter is null || experimenter.UserName is null || experimenter.Email is null) return ResultDTO.Result.ExperimenterDoesNotExist;

        var nameAndId = new ExperimenterDTO.NameAndId
        {
            Id = experimenter.Id,
            UserName = experimenter.UserName
        };

        var token = Token.GenerateToken(nameAndId);

        var result = await SendEmailAsync(Email.Email, Email.NomeEmail, experimenter.Email, experimenter.Name, $"{baseUrl}account/passwordReset/{token}");

        if (!result) return ResultDTO.Result.CouldNotSendEmail;
        else return ResultDTO.Result.Succeeded;
    }

    private async Task<bool> SendEmailAsync(string from, string fromName, string to, string toName, string link)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("EmailClient");

            var messages = new
            {
                Messages = new[]
                {
                        new
                        {
                            From = new { Email = from, Name = fromName },
                            To = new[] { new { Email = to, Name = toName } },
                            Subject = "Recuperação de senha BrewLab",
                            TextPart = "Dear passenger 1, welcome to Mailjet! May the delivery force be with you!",
                            HTMLPart = $"<h3>Olá, {toName}</h3><br />Clique <a href='{link}'>aqui</a> para redefinir sua senha.</br>Caso não tenha sido você que requisitou uma recuperação de senha, ignore este email."
                        }
                }
            };

            var request = JsonSerializer.Serialize(messages);

            var content = new StringContent(request, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("v3.1/send", content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch(Exception ex)
        {
            return false;
        }
    }
}
