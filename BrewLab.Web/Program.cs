using BrewLab.Models;
using BrewLab.Models.Models;
using BrewLab.Services.Services;
using BrewLab.Web;
using BrewLab.Web.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();

builder.Services.AddDbContext<BrewLabContext>();

builder.Services.AddIdentity<Experimenter, IdentityRole<int>>()
    .AddEntityFrameworkStores<BrewLabContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<PasswordHasher<Experimenter>>();
builder.Services.AddScoped<ExperimenterService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddMudServices();

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
