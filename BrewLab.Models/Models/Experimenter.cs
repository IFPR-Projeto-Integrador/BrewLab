using BrewLab.Models.Base;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BrewLab.Models.Models;
public class Experimenter : IdentityUser<int>, IBrewLabModel<int>
{
    [Required]
    [MaxLength(75)]
    public string Name { get; set; } = "";
}
