using BrewLab.Models.Base;
using BrewLab.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace BrewLab.Models.Models;
public class TemperatureLog : IBrewLabModel
{
    [Key]
    public int Id { get; set; }
    [Required]
    public Vase Vase { get; set; }
    [Required]
    public double Reading { get; set; }
    [Required]
    public DateTime ReadAt {  get; set; }

    [Required]
    public int IdExperimentation { get; set; }
    [Required]
    public Experimentation? Experimentation { get; set; }
    
}
