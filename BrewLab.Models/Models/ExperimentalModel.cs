using BrewLab.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace BrewLab.Models.Models;
public class ExperimentalModel : AuditEntity, IBrewLabModel<int>
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = "";
    [Required]
    public string Model { get; set; } = "";
    [Required]
    public string Description { get; set; } = "";

    [Required]
    public int ExperimenterId { get; set; }
    [Required]
    public Experimenter? Experimenter { get; set; }
}
