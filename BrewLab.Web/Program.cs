using BrewLab.Models;
using BrewLab.Models.Models;
using BrewLab.Services;
using BrewLab.Web.Services;
using Microsoft.AspNetCore.Identity;
using MudBlazor.Services;
using BrewLab.Web;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using MudBlazor;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDbContext<BrewLabContext>();

builder.Services.AddIdentity<Experimenter, IdentityRole<int>>()
    .AddEntityFrameworkStores<BrewLabContext>();

builder.Services.AddScoped<PasswordHasher<Experimenter>>();
builder.Services.AddScoped<ExperimenterService>();
builder.Services.AddScoped<ExperimentalModelService>();
builder.Services.AddScoped<ExperimentalPlanningService>();
builder.Services.AddScoped<AuthenticationStateProvider, AuthService>();
builder.Services.AddScoped<AuthService>(sp => (AuthService) sp.GetRequiredService<AuthenticationStateProvider>());
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.BottomRight;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
