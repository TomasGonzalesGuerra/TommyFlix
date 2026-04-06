namespace TommyFlix.Web.Models;

public class TmdbVideosResponse
{
    public int Id { get; set; }
    public List<TmdbVideo>? Results { get; set; }
}