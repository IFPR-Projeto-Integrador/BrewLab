using BrewLab.Models.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewLab.Services.Contracts;
public interface IExperimenterManager
{
    public Task<Experimenter?> FindByNameAsync(string username);
    public Task<IdentityResult> CreateAsync(Experimenter model, string password);
}
