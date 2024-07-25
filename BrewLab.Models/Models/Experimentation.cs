using BrewLab.Models.Base;
using BrewLab.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace BrewLab.Models.Models;
public class Experimentation : AuditEntity, IBrewLabModel
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = "";
    [Required]
    public ExperimentationStatus Status { get; set; }
    [Required]
    public string Description { get; set; } = "";

    [Required]
    public int IdExperiment {  get; set; }
    [Required]
    public Experiment? Experiment { get; set; }
}
