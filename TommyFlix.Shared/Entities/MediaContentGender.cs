namespace TommyFlix.Shared.Entities;

public class MediaContentGender
{
    public MediaContent? MediaContent { get; set; }
    public int MediaContentId { get; set; }

    public Gender? Gender { get; set; }
    public int GenderId { get; set; }
}
