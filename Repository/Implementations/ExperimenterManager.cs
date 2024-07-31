using BrewLab.Models.Models;
using BrewLab.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrewLab.Services.Implementations;
public class ExperimenterManager(IUserStore<Experimenter> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<Experimenter> passwordHasher, IEnumerable<IUserValidator<Experimenter>> userValidators, IEnumerable<IPasswordValidator<Experimenter>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<Experimenter>> logger) : UserManager<Experimenter>(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger), IExperimenterManager
{
    public override async Task<Experimenter?> FindByNameAsync(string userName)
    {
        var experimenter = await base.FindByNameAsync(userName);

        if (experimenter is not null && experimenter.Deleted) return null;

        return experimenter;
    }
}
