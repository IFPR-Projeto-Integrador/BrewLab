using MudBlazor;

namespace BrewLab.Web.Services;

public static class Theme
{
    public static readonly MudTheme AppTheme = new()
    {
        PaletteLight = new PaletteLight()
        {
            Primary = Colors.Pink,

            Surface = Colors.Thistle,
            Background = Colors.Beige,
            AppbarBackground = Colors.Blue,
            DrawerBackground = Colors.Thistle,
        },
    };

    public static class Colors
    {
        public const string Black = "#222222";
        public const string White = "#ffffff";
        public const string Beige = "#f5e6e8";
        public const string Blue = "#192a51";
        public const string Pink = "#967aa1";
        public const string Thistle = "#d5c6e0";
    }
}
