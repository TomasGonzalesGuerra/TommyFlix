namespace TommyFlix.Shared.Entities;

public class MediaContentTag
{
    public MediaContent? MediaContent { get; set; }
    public int MediaContentId { get; set; }

    public Tag? Tag { get; set; }
    public int TagId { get; set; }
}
