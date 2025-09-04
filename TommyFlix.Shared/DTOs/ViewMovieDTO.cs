namespace TommyFlix.Shared.DTOs;

public class ViewMovieDTO
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? PosterUrl { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public TimeSpan Duration { get; set; }
    public List<int>? Tags { get; set; }
    public List<int>? Genders { get; set; }
}
