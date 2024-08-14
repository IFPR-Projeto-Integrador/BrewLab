using BrewLab.Common.DTOs;
using BrewLab.Models;
using BrewLab.Models.Models;
using BrewLab.Repository.Base;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices.JavaScript;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace BrewLab.Services;
public class ExperimentService(
    BrewLabContext context,
    ExperimentalPlanningService experimentalPlanningService,
    ExperimenterService experimenterService,
    ExperimentalModelService experimentalModelService
    ) : Repository<Experiment>(context)
{
    private readonly ExperimentalPlanningService _experimentalPlanningService = experimentalPlanningService;
    private readonly ExperimenterService _experimenterService = experimenterService;
    private readonly ExperimentalModelService _experimentalModelService = experimentalModelService;

    public async Task<ResultDTO.Result> Create(int experimentalPlanningId, int experimenterId)
    {
        var experimentalPlanning = await _experimentalPlanningService.GetExperimentalPlanningById(experimentalPlanningId, experimenterId);

        if (experimentalPlanning is null) return ResultDTO.Result.InvalidIdentification;
        if (!_experimenterService.Exists(experimenterId)) return ResultDTO.Result.InvalidIdentification;

        var experimentalModel = await _experimentalModelService.GetExperimentalModelById(experimentalPlanning.IdExperimentalModel!.Value, experimenterId);

        var experiments = PlanningToExperiments(experimentalModel!.Model, experimentalPlanning.ExperimentalMatrix);

        if (experiments is null) return ResultDTO.Result.ParsingFail;

        foreach (var experiment in experiments)
        {
            var experimentToAdd = new Experiment()
            {
                ExperimentalPlanningId = experimentalPlanningId,
                ParsedModel = experiment,
            };

            await Update(experimentToAdd);
        }

        return ResultDTO.Result.Succeeded;
    }

    public async Task<bool?> HasExperiments(int experimentalPlanningId, int experimenterId)
    {
        if (await _experimentalPlanningService.GetExperimentalPlanningById(experimentalPlanningId, experimenterId) is null) return null;

        return await Get<Experiment>().AnyAsync();
    }

    private static List<string>? PlanningToExperiments(string model, string jsonData)
    {
        var json = JsonNode.Parse(jsonData);
        if (json is null) return null;

        var jsonArray = json as JsonArray;
        if (jsonArray is null) return null;

        var endResult = new List<string>();

        if (jsonArray is null) return null;

        foreach (var arrayElement in jsonArray)
        {
            string result = model;

            if (arrayElement is not JsonObject jsonObject) return null;

            foreach (var property in jsonObject)
            {
                string key = "@" + property.Key;
                string? value = property.Value?.ToString();

                if (value is null) return null;

                result = result.Replace(key, value);
            }

            endResult.Add(result);
        }

        return endResult;
    }
}
