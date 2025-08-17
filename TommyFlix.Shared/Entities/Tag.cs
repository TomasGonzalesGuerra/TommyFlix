using System.ComponentModel.DataAnnotations;

namespace TommyFlix.Shared.Entities;

public class Tag
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public string? Name { get; set; }
}
