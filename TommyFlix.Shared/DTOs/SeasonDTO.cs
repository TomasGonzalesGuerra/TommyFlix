namespace TommyFlix.Shared.DTOs;

public class SeasonDTO
{
    public int Id { get; set; }
    public int SeasonNumber { get; set; }
    public List<int>? Episodes { get; set; }
}