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

    public bool ExperimenterExists(int id) => Exists(e => e.Id == id);
}
