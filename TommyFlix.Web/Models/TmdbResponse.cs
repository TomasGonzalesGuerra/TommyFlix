namespace TommyFlix.Web.Models;

public class TmdbResponse<T>
{
    public int Page { get; set; }
    public List<T>? Results { get; set; }
    public int Total_Results { get; set; }
    public int Total_Pages { get; set; }
}