namespace TommyFlix.Shared.Entities;

public class WatchHistory
{
    public int Id { get; set; }
    public DateTime WatchedAt { get; set; }

    public User? User { get; set; }
    public string? UserId { get; set; }
    public MediaContent? MediaContent { get; set; }
    public int MediaContentId { get; set; }
}
