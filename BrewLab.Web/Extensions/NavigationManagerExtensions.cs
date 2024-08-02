using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace BrewLab.Web.Extensions;

public static class NavigationManagerExtensions
{
    public static void ToReturnUrl(this NavigationManager manager)
    {
        var uri = new Uri(manager.Uri);

        var query = QueryHelpers.ParseQuery(uri.Query);

        if (query.TryGetValue("ReturnUrl", out var values))
        {
            var parameterValue = values.FirstOrDefault();

            if (parameterValue is null) return;

            manager.NavigateTo($"{parameterValue}");
        }
        else
        {
            return;
        }
    }
}
