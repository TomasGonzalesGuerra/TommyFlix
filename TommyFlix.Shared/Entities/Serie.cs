namespace TommyFlix.Shared.Entities;

public class Serie
{
    public int Id { get; set; }
    public int TmdbId { get; set; }  // único vínculo con TMDB

    public ICollection<UserRating>? Ratings { get; set; }
    public ICollection<WatchHistory>? WatchHistories { get; set; }
    public ICollection<UserFavorite>? Favorites { get; set; }
}