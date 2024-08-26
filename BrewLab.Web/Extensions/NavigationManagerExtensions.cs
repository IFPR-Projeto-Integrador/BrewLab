using BrewLab.Web.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;

namespace BrewLab.Web.Extensions;

public static class NavigationManagerExtensions
{
    public static void ToReturnUrl(this NavigationManager manager, AuthService authService)
    {
        var uri = new Uri(manager.Uri);

        var query = QueryHelpers.ParseQuery(uri.Query);

        if (query.TryGetValue("ReturnUrl", out var values))
        {
            var parameterValue = values.FirstOrDefault();

            if (parameterValue is null) return;

            manager.NavigateTo($"{parameterValue}");
        }

        authService.HasAttemptedAuthentication = true;
    }
}
