namespace TommyFlix.Shared.Entities;

public class UserFavorite
{
    public int Id { get; set; }
    public DateTime AddedAt { get; set; } = DateTime.UtcNow;

    public User? User { get; set; }
    public string? UserId { get; set; }

    public Movie? Movie { get; set; }
    public int? MovieId { get; set; }

    public Serie? Serie { get; set; }
    public int? SerieId { get; set; }
}