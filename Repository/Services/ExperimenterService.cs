using BrewLab.Common.DTOs;
using BrewLab.Common.DTOs.Results;
using BrewLab.Models;
using BrewLab.Models.Models;
using BrewLab.Repository.Base;
using BrewLab.Services.Contracts;
using Microsoft.AspNetCore.Identity;

namespace BrewLab.Services.Services;
public class ExperimenterService(
    BrewLabContext context,
    IExperimenterSignInManager signInManager,
    IExperimenterManager userManager) : Repository<Experimenter>(context)
{
    private readonly IExperimenterSignInManager _signInManager = signInManager;
    private readonly IExperimenterManager _userManager = userManager;

    public async Task<bool> Login(ExperimenterDTO.Login login)
    {
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
