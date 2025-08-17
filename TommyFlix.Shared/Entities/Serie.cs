using System.ComponentModel.DataAnnotations;

namespace TommyFlix.Shared.Entities;

public class Serie
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? PosterUrl { get; set; }
    public DateTime? ReleaseDate { get; set; }

    public ICollection<Tag>? Tags { get; set; }
    public ICollection<Gender>? Genders { get; set; }
    public ICollection<Season>? Seasons { get; set; }
    public ICollection<CastMember>? Cast { get; set; }
    public ICollection<UserRating>? Ratings { get; set; }
    public ICollection<WatchHistory>? WatchHistories { get; set; }
}
