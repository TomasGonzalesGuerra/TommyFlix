namespace TommyFlix.Shared.Entities;

public class Serie : MediaContent
{
    public ICollection<Season>? Seasons { get; set; }
}
