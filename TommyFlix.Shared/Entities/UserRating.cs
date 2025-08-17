namespace TommyFlix.Shared.Entities;

public class UserRating
{
    public int Id { get; set; }
    public int Rating { get; set; }
    public string? Review { get; set; }

    public User? User { get; set; }
    public string? UserId { get; set; }
    public MediaContent? MediaContent { get; set; }
    public int MediaContentId { get; set; }
}
