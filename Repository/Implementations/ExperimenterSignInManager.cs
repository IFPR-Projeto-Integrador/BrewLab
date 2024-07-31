using BrewLab.Models.Models;
using BrewLab.Services.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BrewLab.Services.Implementations;
public class ExperimenterSignInManager(UserManager<Experimenter> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<Experimenter> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<Experimenter>> logger, IAuthenticationSchemeProvider schemes) : SignInManager<Experimenter>(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes), IExperimenterSignInManager
{
    public override async Task<SignInResult> CheckPasswordSignInAsync(Experimenter user, string password, bool lockoutOnFailure)
    {
        if (user.Deleted) return SignInResult.Failed;

        return await base.CheckPasswordSignInAsync(user, password, lockoutOnFailure);
    }

    public override async Task SignInAsync(Experimenter user, bool isPersistent, string? authenticationMethod = null)
    {
        if (user.Deleted) return;

        await base.SignInAsync(user, isPersistent, authenticationMethod);
    }

    
}
