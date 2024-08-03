using BrewLab.Common.DTOs;
using BrewLab.Models;
using BrewLab.Models.Models;
using BrewLab.Repository.Base;

namespace BrewLab.Services.Services;
public class ExperimentalModelService(
    BrewLabContext context,
    ExperimenterService experimenterService) : Repository<ExperimentalModel>(context)
{
    private ExperimenterService _experimenterService = experimenterService;
    public async Task<ResultDTO.Result> CreateExperimentalModel(ExperimentalModelDTO.Create create)
    {
        ArgumentNullException.ThrowIfNull(nameof(create));

        if (!_experimenterService.ExperimenterExists(create.ExperimenterId)) return ResultDTO.Result.InvalidIdentification;
        if (!create.Validate()) return ResultDTO.Result.InvalidDTO;

        var model = new ExperimentalModel
        {
            Name = create.Name,
            Model = create.Model,
            Description = create.Description,
            ExperimenterId = create.ExperimenterId,
        };

        await Insert(model);

        return ResultDTO.Result.Succeeded;
    }

    public IEnumerable<ExperimentalModelDTO.View>? GetExperimentalModelsByExperimenterId(int experimenterId)
    {
        if (!_experimenterService.ExperimenterExists(experimenterId)) return null;

        var dbModels = Find(m => m.ExperimenterId == experimenterId);
        var returnModels = dbModels.Select(m => new ExperimentalModelDTO.View
        {
            Id = m.Id,
            Name = m.Name,
            Model = m.Model,
            Description = m.Description,
        });

        return returnModels;
    }

    public async Task<ExperimentalModelDTO.View?> GetExperimentalModelById(int id)
    {
        var dbModel = await FindSingle(m => m.Id == id);

        if (dbModel is null) return null;

        return new ExperimentalModelDTO.View
        {
            Id = dbModel.Id,
            Name = dbModel.Name,
            Model = dbModel.Model,
            Description = dbModel.Description,
        };
    }

    public async Task<ResultDTO.Result> EditExperimentalModel(ExperimentalModelDTO.Edit edit)
    {
        if (!edit.Validate()) return ResultDTO.Result.InvalidDTO;

        var dbModel = await FindSingle(m => m.Id == edit.Id);

        if (dbModel is null) return ResultDTO.Result.InvalidIdentification;

        dbModel.Name = edit.Name;
        dbModel.Description = edit.Description;
        dbModel.Model = edit.Model;

        await Update(dbModel);

        return ResultDTO.Result.Succeeded;
    }

    public async Task<ResultDTO.Result> DeleteExperimentalModel(int id)
    {
        var dbModel = await FindSingle(m => m.Id == id);

        if (dbModel is null) return ResultDTO.Result.InvalidIdentification;

        await Delete(dbModel);

        return ResultDTO.Result.Succeeded;
    }
}
