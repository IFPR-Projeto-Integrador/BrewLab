using BrewLab.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace BrewLab.Models.Models;
public class Experimenter : AuditEntity, IBrewLabModel
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(128)]
    public string Login { get; set; } = "";
    [Required]
    [MaxLength(75)]
    public string Name { get; set; } = "";
    [Required]
    [MaxLength(75)]
    public string Email { get; set; } = "";
    [Required]
    [MaxLength(128)]
    public string Password { get; set; } = "";
}
