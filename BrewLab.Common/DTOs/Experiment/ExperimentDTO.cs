namespace BrewLab.Common.DTOs;
public static class ExperimentDTO
{
    public class View
    {
        public int Id { get; set; }
        public ExperimentalPlanningDTO.View ExperimentalPlanning { get; set; } = new();
    }
}
