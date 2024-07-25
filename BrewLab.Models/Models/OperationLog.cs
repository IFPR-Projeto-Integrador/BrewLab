using BrewLab.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace BrewLab.Models.Models;
public class OperationLog : IBrewLabModel
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Operation { get; set; } = "";
    [Required]
    public DateTime? ReadAt { get; set; } = DateTime.Now;

    [Required]
    public int IdExperimentation {  get; set; }
    [Required]
    public Experimentation? Experimentation { get; set; }
}
