using System.ComponentModel.DataAnnotations;

namespace TommyFlix.Shared.Entities;

public abstract class MediaContent
{
    public int Id { get; set; }
    [Required(ErrorMessage = "El campo {0} es obligatorio.")]
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? PosterUrl { get; set; }
    public DateTime? ReleaseDate { get; set; }

    public ICollection<CastMember>? Cast { get; set; }
    public ICollection<UserRating>? Ratings { get; set; }
    public ICollection<WatchHistory>? WatchHistories { get; set; }
    public ICollection<MediaContentTag>? MediaContentTags { get; set; }
    public ICollection<MediaContentGender>? MediaContentGenders { get; set; }
}
