using BrewLab.Common.DTOs;
using BrewLab.Common.JWT;
using BrewLab.Models;
using BrewLab.Models.Models;
using BrewLab.Repository.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BrewLab.Services;
public class ExperimenterService(
    BrewLabContext context,
    PasswordHasher<Experimenter> hasher,
    UserManager<Experimenter> userManager) : Repository<Experimenter>(context)
{
    private readonly PasswordHasher<Experimenter> _hasher = hasher;
    private readonly UserManager<Experimenter> _userManager = userManager;

    public async Task<ResultDTO.Result> AttemptUpdate(ExperimenterDTO.Account account)
    {
        avar experimenter = await FindSingle(e => e.Id == account.Id);

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

        return new ResultDTO.Auth
        {
            Success = result.Succeeded,
            Errors = result.Errors.Select(e => e.Description),
            Token = Token.GenerateToken(nameAndId),
            Experimenter = nameAndId
        };
    }
}
