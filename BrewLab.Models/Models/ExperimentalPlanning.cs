using BrewLab.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace BrewLab.Models.Models;
public class ExperimentalPlanning : IBrewLabModel<int>, ILoggedEntity, IVirtualDeleteable
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
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    [Required]
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    [Required]
    public bool Deleted { get; set; } = false;

    [Required]
    public int ExperimentalModelId { get; set; }
    [Required]
    public ExperimentalModel? ExperimentalModel { get; set; }

    public ICollection<Experiment>? Experiments { get; set; }

}
