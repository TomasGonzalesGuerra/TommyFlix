namespace TommyFlix.Shared.Entities;

public class UserRating
{
    public int Id { get; set; }
    public int Rating { get; set; }       // ej: 1-10
    public string? Review { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public User? User { get; set; }
    public string? UserId { get; set; }

    // Solo uno de los dos tendrá valor, el otro será null
    public Movie? Movie { get; set; }
    public int? MovieId { get; set; }

    public Serie? Serie { get; set; }
    public int? SerieId { get; set; }
}
