using BrewLab.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace BrewLab.Models.Models;
public class Experiment : AuditEntity, IBrewLabModel<int>
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string ParsedModel { get; set; } = "";

    [Required]
    public int IdExperimentalPlanning { get; set; }
    [Required]
    public ExperimentalPlanning? ExperimentalPlanning { get; set; }
}
