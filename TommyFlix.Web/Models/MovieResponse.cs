namespace TommyFlix.Web.Models;

public class MovieResponse
{
    public int Page { get; set; }
    public List<Movie> Results { get; set; }
    public int Total_Pages { get; set; }
}