using System.ComponentModel.DataAnnotations;

namespace TommyFlix.Shared.Entities;

public class Season
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public int SeasonNumber { get; set; }

    public Serie? Serie { get; set; }
    public int SerieId { get; set; }

    public ICollection<Episode>? Episodes { get; set; }
}