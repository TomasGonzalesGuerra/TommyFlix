using System.ComponentModel.DataAnnotations;

namespace TommyFlix.Shared.Entities;

public class Episode
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public string? Title { get; set; }
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public int EpisodeNumber { get; set; }
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public TimeSpan Duration { get; set; }

    public Season? Season { get; set; }
    public int SeasonId { get; set; }
}
