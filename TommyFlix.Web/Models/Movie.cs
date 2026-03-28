namespace TommyFlix.Web.Models;

public class Movie
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Overview { get; set; }
    public string? Release_Date { get; set; }
    public string? Poster_Path { get; set; }
    public string? Backdrop_Path { get; set; }
    public double Vote_Average { get; set; }
    public int Runtime { get; set; }                        // minutos
    public List<Genre>? Genres { get; set; }
    public List<ProductionCompany>? Production_Companies { get; set; }
    public string? Tagline { get; set; }
    public string? Status { get; set; }
    public Credits? Credits { get; set; }                   // cast y crew
}