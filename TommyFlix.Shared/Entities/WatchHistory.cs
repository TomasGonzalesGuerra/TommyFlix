namespace TommyFlix.Shared.Entities;

public class WatchHistory
{
    public int Id { get; set; }
    public DateTime WatchedAt { get; set; }

    public User? User { get; set; }
    public string? UserId { get; set; }
    public Movie? Movie { get; set; }
    public int MovieId { get; set; }
    public Serie? Serie { get; set; }
    public int SerieId { get; set; }
}
