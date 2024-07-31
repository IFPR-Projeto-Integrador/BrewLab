using System.ComponentModel.DataAnnotations;

namespace BrewLab.Models.Base;
public interface ILoggedEntity
{
    [Required]
    public DateTime? CreatedAt { get; set; }
    [Required]
    public DateTime? UpdatedAt { get; set; }
}
