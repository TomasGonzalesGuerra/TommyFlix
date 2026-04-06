namespace TommyFlix.Web.Models;

public class TmdbVideo
{
    public string? Key { get; set; }       // clave de YouTube
    public string? Site { get; set; }      // "YouTube" o "Vimeo"
    public string? Type { get; set; }      // "Trailer", "Teaser", "Clip", etc.
    public string? Name { get; set; }
    public bool Official { get; set; }
}