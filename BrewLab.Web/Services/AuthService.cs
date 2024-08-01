using BrewLab.Common.DTOs;
using BrewLab.Services.Services;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BrewLab.Web.Services;

public class AuthService(ExperimenterService experimenterRepo) : AuthenticationStateProvider
{
    private readonly ExperimenterService _experimenterService = experimenterRepo;

    public string Token { get; set; } = "";

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var principal = new ClaimsPrincipal();

        if (string.IsNullOrWhiteSpace(Token)) return new AuthenticationState(principal);

        var user = await _experimenterService.Validate(Token);

        if (user is null) return new AuthenticationState(principal);

        List<Claim> claims = [
            new Claim("Username", user.UserName),
            new Claim("Id", user.Id.ToString())
            ];

        var identity = new ClaimsIdentity(claims, "LocalStorage");

        principal.AddIdentity(identity);

        return new AuthenticationState(principal);
    }

    public async Task<ResultDTO.Auth> Login(ExperimenterDTO.Login login)
    {
        var result = await _experimenterService.Login(login);

        if (!result.Success) return result;

        Token = result.Token!;

        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());

        return result;
    }

    public void Logout()
    {
        Token = "";
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task<ResultDTO.Auth> Register(ExperimenterDTO.Register register)
    {
        return await _experimenterService.Register(register);
    }
}
