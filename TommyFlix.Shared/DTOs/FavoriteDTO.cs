namespace TommyFlix.Shared.DTOs;

public class FavoriteDTO
{
    public int TmdbId { get; set; }
    public string Type { get; set; } = "movie"; // "movie" o "serie"
    public DateTime AddedAt { get; set; }
}