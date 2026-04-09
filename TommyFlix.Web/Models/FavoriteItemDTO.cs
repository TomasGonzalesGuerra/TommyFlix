namespace TommyFlix.Web.Models;

public class FavoriteItemDTO
{
    public int Id { get; set; }
    public int? MovieId { get; set; }
    public int? MovieTmdbId { get; set; }
    public int? SerieId { get; set; }
    public int? SerieTmdbId { get; set; }
    public DateTime AddedAt { get; set; }
}