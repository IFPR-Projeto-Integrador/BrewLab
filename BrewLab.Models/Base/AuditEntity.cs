using System.ComponentModel.DataAnnotations;

namespace BrewLab.Models.Base;
public class AuditEntity : ILoggedEntity, IVirtualDeleteable
{
    [Required]
    public DateTime? CreatedAt { get; set; } = DateTime.Now;
    [Required]
    public DateTime? UpdatedAt { get; set; } = DateTime.Now;
    [Required]
    public bool Deleted { get; set; } = false;
    
}
