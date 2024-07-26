using BrewLab.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace BrewLab.Models.Models;
public class ExperimentalPlanning : AuditEntity, IBrewLabModel<int>
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = "";
    [Required]
    public string ExperimentalMatrix { get; set; } = "";
    [Required]
    public string Description { get; set; } = "";

    [Required]
    public int IdExperimentalModel { get; set; }
    [Required]
    public ExperimentalModel? ExperimentalModel { get; set; }

}
