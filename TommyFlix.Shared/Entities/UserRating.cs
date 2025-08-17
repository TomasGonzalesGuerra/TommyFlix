namespace TommyFlix.Shared.Entities;

public class UserRating
{
    public int Id { get; set; }
    public int Rating { get; set; }
    public string? Review { get; set; }

    public User? User { get; set; }
    public string? UserId { get; set; }
    public Movie? Movie { get; set; }
    public int MovieId { get; set; }
    public Serie? Serie { get; set; }
    public int SerieId { get; set; }
}
