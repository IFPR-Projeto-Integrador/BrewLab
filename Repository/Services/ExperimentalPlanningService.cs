using BrewLab.Common.DTOs;
using BrewLab.Models;
using BrewLab.Models.Models;
using BrewLab.Repository.Base;
using Microsoft.EntityFrameworkCore;

namespace BrewLab.Services.Services;
public class ExperimentalPlanningService(
    BrewLabContext context,
    ExperimentalModelService experimentalModelService,
    ExperimenterService experimenterService) : Repository<ExperimentalPlanning>(context)
{
    private ExperimentalModelService _experimentalModelService = experimentalModelService;
    private ExperimenterService _experimenterService = experimenterService;

    public async Task<ResultDTO.Result> CreateExperimentalPlanning(ExperimentalPlanningDTO.Create create)
    {
        ArgumentNullException.ThrowIfNull(nameof(create));

        if (!_experimentalModelService.Exists(create.IdExperimentalModel)) return ResultDTO.Result.InvalidIdentification;
        if (!create.Validate()) return ResultDTO.Result.InvalidDTO;

        var planning = new ExperimentalPlanning
        {
            Name = create.Name,
            ExperimentalMatrix = create.ExperimentalMatrix,
            Description = create.Description,
            ExperimentalModelId = create.IdExperimentalModel,
        };

        await Insert(planning);

        return ResultDTO.Result.Succeeded;
    }

    public async Task<IEnumerable<ExperimentalPlanningDTO.ViewWithExperimentalModels>?> GetExperimentalPlanningsByExperimenterIdAsync(int experimenterId)
    {
        if (!_experimenterService.Exists(em => em.Id == experimenterId)) return null;

        var dbPlannings = Get<ExperimentalPlanning>()
            .Where(e => e.ExperimentalModel!.ExperimenterId == experimenterId)
            .Include(e => e.ExperimentalModel);
            

        return await dbPlannings.Select(ep => new ExperimentalPlanningDTO.ViewWithExperimentalModels
        {
            Id = ep.Id,
            Name = ep.Name,
            ExperimentalMatrix = ep.ExperimentalMatrix,
            Description = ep.Description,
            ExperimentalModel = new ExperimentalModelDTO.View
            {
                Id = ep.ExperimentalModel!.Id,
                Name = ep.ExperimentalModel.Name,
                Description = ep.ExperimentalModel.Description,
            }
        }).ToListAsync();
    }

    public async Task<ExperimentalPlanningDTO.View?> GetExperimentalPlanningById(int id, int experimenterId)
    {
        var dbPlanning = await Get<ExperimentalPlanning>()
            .FirstOrDefaultAsync(m => m.Id == id && m.ExperimentalModel!.ExperimenterId == experimenterId);

        if (dbPlanning is null) return null;

        return new ExperimentalPlanningDTO.View
        {
            Id = dbPlanning.Id,
            Name = dbPlanning.Name,
            ExperimentalMatrix = dbPlanning.ExperimentalMatrix,
            Description = dbPlanning.Description,
            IdExperimentalModel = dbPlanning.ExperimentalModelId
        };
    }

    public async Task<ResultDTO.Result> DeleteExperimentalPlanning(int id, int experimenterId)
    {
        var dbPlanning = await FindSingle(m => m.Id == id && m.ExperimentalModel?.ExperimenterId == experimenterId);

        if (dbPlanning is null) return ResultDTO.Result.InvalidIdentification;

        await Delete(dbPlanning);

        return ResultDTO.Result.Succeeded;
    }
}
