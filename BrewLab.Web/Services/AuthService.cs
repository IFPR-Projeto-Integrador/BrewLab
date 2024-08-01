using BrewLab.Common.DTOs;
using BrewLab.Common.DTOs.Results;
using BrewLab.Models.Models;
using BrewLab.Services.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BrewLab.Web.Services;

public class AuthService(ExperimenterService experimenterRepo, SignInManager<Experimenter> signInManager)
{
    private readonly ExperimenterService _experimenterRepo = experimenterRepo;
    private readonly SignInManager<Experimenter> _signInManager = signInManager;

    public async Task<bool> Login(ExperimenterDTO.Login login)
    {
        var result = await _experimenterRepo.GetUser(login);

        if (result is null) return false;

        await _signInManager.SignInAsync(result, false);

        return true;
    }

    public async Task Logout(HttpContext context)
    {
        if (context.User.Identity is not null && context.User.Identity.IsAuthenticated)
        {
            await context.SignOutAsync();
        }
    }

    public async Task<ResultDTO.Auth> Register(ExperimenterDTO.Register register)
    {
        return new();
    }
}
