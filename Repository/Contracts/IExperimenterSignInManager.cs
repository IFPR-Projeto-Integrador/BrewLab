using BrewLab.Models.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewLab.Services.Contracts;
public interface IExperimenterSignInManager
{
    public Task<SignInResult> CheckPasswordSignInAsync(Experimenter user, string password, bool lockoutOnFailure);
    public Task SignInAsync(Experimenter user, bool isPersistent, string? authenticationMethod = null);
    
}
