namespace TommyFlix.Shared.DTOs;

public class CreateSerieDTO
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? PosterUrl { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public List<int>? Genders { get; set; }
    public List<int>? Tags { get; set; }
}