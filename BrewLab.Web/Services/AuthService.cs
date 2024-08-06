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

    public bool HasAttemptedAuthentication { get; set; } = false;
    public ExperimenterDTO.NameAndId? User { get; set; }

    public string Token { get; set; } = "";

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var principal = new ClaimsPrincipal();

        if (string.IsNullOrWhiteSpace(Token)) return new AuthenticationState(principal);

        var user = await _experimenterService.Validate(Token);

        if (user is null)
        {
            return new AuthenticationState(principal);
        }

        User = user;

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
        User = result.Experimenter;

        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        HasAttemptedAuthentication = true;

        return result;
    }

    public async Task<ResultDTO.Auth> Register(ExperimenterDTO.Register register)
    {
        var result = await _experimenterService.Register(register);

        if (!result.Success) return result;

        Token = result.Token!;
        User = result.Experimenter;

        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        HasAttemptedAuthentication = true;

        return result;
    }

    public async Task<ExperimenterDTO.NameAndId?> Validate(string token)
    {
        var result = await _experimenterService.Validate(token);

        if (result is null) return null;

        Token = token;
        User = result;

        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        HasAttemptedAuthentication = true;

        return result;
    }

    public void Logout()
    {
        Token = "";
        User = null;
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        HasAttemptedAuthentication = true;
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

    // Não chamar antes do ciclo de vida de pós-renderização.
    public async Task DeleteTokenLocalStorage()
    {
        await _localStorage.DeleteAsync("Token");
    }
}
