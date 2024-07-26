using BrewLab.Models.Base;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BrewLab.Models.Models;
public class Experimenter : IdentityUser<int>, IBrewLabModel<int>, ILoggedEntity, IVirtualDeleteable
{
    [Required]
    [MaxLength(75)]
    public string Name { get; set; } = "";
    [Required]
    public DateTime? CreatedAt { get; set; } = DateTime.Now;
    [Required]
    public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    [Required]
    public bool Deleted { get; set; } = false;
}
