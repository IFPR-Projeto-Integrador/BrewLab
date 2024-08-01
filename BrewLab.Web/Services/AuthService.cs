using BrewLab.Common.DTOs;
using BrewLab.Services.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;

namespace BrewLab.Web.Services;

public class AuthService(ExperimenterService experimenterRepo, ProtectedLocalStorage localStorage) : AuthenticationStateProvider
{
    private readonly ExperimenterService _experimenterService = experimenterRepo;
    private readonly ProtectedLocalStorage _localStorage = localStorage;

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

    public async Task<ExperimenterDTO.NameAndId?> Validate(string token)
    {
        var result = await _experimenterService.Validate(token);

        if (result is null) return null;

        Token = token;

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

    // Não chamar antes do ciclo de vida de pós-renderização.
    public async Task<string?> GetTokenLocalStorage()
    {
        var token = await localStorage.GetAsync<string>("Token");
        if (!token.Success) return null;

        return token.Value!;
    }

    // Não chamar antes do ciclo de vida de pós-renderização.
    public async Task SetTokenLocalStorage(string token)
    {
        await _localStorage.SetAsync("Token", token);
    }
}
