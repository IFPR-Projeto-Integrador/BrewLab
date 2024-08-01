using BrewLab.Common.DTOs;
using BrewLab.Common.JWT;
using BrewLab.Models;
using BrewLab.Models.Models;
using BrewLab.Repository.Base;
using Microsoft.AspNetCore.Identity;

namespace BrewLab.Services.Services;
public class ExperimenterService(
    BrewLabContext context,
    PasswordHasher<Experimenter> hasher,
    UserManager<Experimenter> userManager) : Repository<Experimenter>(context)
{
    private readonly PasswordHasher<Experimenter> _hasher = hasher;
    private readonly UserManager<Experimenter> _userManager = userManager;

    public async Task<ResultDTO.Auth> Login(ExperimenterDTO.Login login)
    {
        var experimenter = await FindSingle(e => e.UserName == login.UserName);

        if (experimenter is null || experimenter.PasswordHash is null || experimenter.UserName is null)
        {
            return ResultDTO.Auth.LoginOuSenhaIncorretos;
        }

        var result = _hasher.VerifyHashedPassword(experimenter, experimenter.PasswordHash, login.Password);

        if (result == PasswordVerificationResult.Success)
        {
            return new ResultDTO.Auth
            {
                Success = true,
                Token = Token.GenerateToken(new ExperimenterDTO.NameAndId()
                { Id = experimenter.Id, UserName = experimenter.UserName })
            };
        }
        else
            return ResultDTO.Auth.LoginOuSenhaIncorretos;
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

        return new ResultDTO.Auth
        {
            Success = result.Succeeded,
            Errors = result.Errors.Select(e => e.Description),
            Token = Token.GenerateToken(new ExperimenterDTO.NameAndId 
            { Id = experimenterModel.Id, UserName = experimenterModel.UserName })
        };
    }

    public async Task<ExperimenterDTO.NameAndId?> Validate(string token)
    {
        return await Token.ReadToken(token);
    }
}
