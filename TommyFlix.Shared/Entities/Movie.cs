using System.ComponentModel.DataAnnotations;

namespace TommyFlix.Shared.Entities;

public class Movie : MediaContent
{
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public TimeSpan Duration { get; set; }
}
