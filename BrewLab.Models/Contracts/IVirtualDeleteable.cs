using System.ComponentModel.DataAnnotations;

namespace BrewLab.Models.Base;
public interface IVirtualDeleteable
{
    [Required]
    public bool Deleted { get; set; }
}
