using System.ComponentModel.DataAnnotations;
using TommyFlix.Shared.Enums;

namespace TommyFlix.Shared.Entities;

public class CastMember
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public string? FullName { get; set; }
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public CastType Role { get; set; }
    public string? PhotoUrl { get; set; }
}
