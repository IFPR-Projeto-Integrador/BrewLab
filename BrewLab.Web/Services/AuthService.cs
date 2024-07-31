using BrewLab.Common.DTOs;
using BrewLab.Common.DTOs.Results;
using BrewLab.Services.Services;

namespace BrewLab.Web.Services;

public class AuthService(ExperimenterService experimenterRepo)
{
    private readonly ExperimenterService _experimenterRepo = experimenterRepo;

    public async Task<bool> Login(ExperimenterDTO.Login login)
    {
        return await _experimenterRepo.Login(login);
    }

    public async Task<ResultDTO.Auth> Register(ExperimenterDTO.Register register)
    {
        return await _experimenterRepo.Register(register);
    }
}
