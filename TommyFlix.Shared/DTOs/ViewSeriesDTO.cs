namespace TommyFlix.Shared.DTOs;

public class ViewSeriesDTO
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? PosterUrl { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public List<int>? Seasons { get; set; }
    public List<int>? Genders { get; set; }
    public List<int>? Tags { get; set; }
    public List<int>? Ratings { get; set; }
    public List<int>? WatchHistories { get; set; }
    public List<int>? Cast { get; set; }
}
