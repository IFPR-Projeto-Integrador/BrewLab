using BrewLab.Common.DTOs;
using BrewLab.Common.DTOs.Results;
using BrewLab.Models;
using BrewLab.Models.Models;
using BrewLab.Repository.Base;
using Microsoft.AspNetCore.Identity;

namespace BrewLab.Services.Services;
public class ExperimenterService(
    BrewLabContext context,
    SignInManager<Experimenter> signInManager,
    UserManager<Experimenter> userManager) : Repository<Experimenter>(context)
{
    private readonly SignInManager<Experimenter> _signInManager = signInManager;
    private readonly UserManager<Experimenter> _userManager = userManager;

    public async Task<bool> Login(ExperimenterDTO.Login login)
    {
        if (IsDeleted(e => e.UserName == login.UserName)) return false;

        var experimenter = await _userManager.FindByNameAsync(login.UserName);

        if (experimenter is null) return false;

        var result = await _signInManager.CheckPasswordSignInAsync(experimenter, login.Password, false);

        if (result.Succeeded)
            await _signInManager.SignInAsync(experimenter, true);
        else
            return false;

        return true;
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
            Errors = result.Errors.Select(e => (e.Description, e.Code))
        };
    }
}
