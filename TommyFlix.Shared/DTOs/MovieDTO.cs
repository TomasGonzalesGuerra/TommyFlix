using TommyFlix.Shared.Entities;

namespace TommyFlix.Shared.DTOs;

public class MovieDTO
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? PosterUrl { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public TimeSpan Duration { get; set; }
    public List<Tag>? Tags { get; set; }
    public List<Gender>? Genders { get; set; }
    public List<CastMember>? Cast { get; set; }
    public List<UserRating>? Ratings { get; set; }
    public List<WatchHistory>? WatchHistories { get; set; }
}
