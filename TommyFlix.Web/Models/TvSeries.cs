namespace TommyFlix.Web.Models;

public class TvSeries
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Overview { get; set; }
    public string Poster_Path { get; set; }
    public string Backdrop_Path { get; set; }
    public double Vote_Average { get; set; }
}
