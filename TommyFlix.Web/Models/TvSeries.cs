namespace TommyFlix.Web.Models;

public class TvSeries
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Overview { get; set; }
    public string? First_Air_Date { get; set; }
    public string? Poster_Path { get; set; }
    public string? Backdrop_Path { get; set; }
    public double Vote_Average { get; set; }
    public int Number_Of_Seasons { get; set; }
    public int Number_Of_Episodes { get; set; }
    public List<Genre>? Genres { get; set; }
    public string? Tagline { get; set; }
    public string? Status { get; set; }
    public Credits? Credits { get; set; }
    public List<Season>? Seasons { get; set; }
}
