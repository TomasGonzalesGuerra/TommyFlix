using Blazored.LocalStorage;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using TommyFlix.Web.Models;

namespace TommyFlix.Web.Services;

public class FavoriteService(HttpClient http, ILocalStorageService localStorage)
{
    private readonly HttpClient _http = http;
    private readonly ILocalStorageService _localStorage = localStorage;

    private async Task SetAuthHeaderAsync()
    {
        var token = await _localStorage.GetItemAsync<string>("authToken");
        if (!string.IsNullOrEmpty(token))
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    public async Task<bool> IsMovieFavoriteAsync(int tmdbId)
    {
        await SetAuthHeaderAsync();
        return await _http.GetFromJsonAsync<bool>($"api/favorites/movie/{tmdbId}");
    }

    public async Task<bool> IsSerieFavoriteAsync(int tmdbId)
    {
        await SetAuthHeaderAsync();
        return await _http.GetFromJsonAsync<bool>($"api/favorites/serie/{tmdbId}");
    }

    public async Task<bool> ToggleMovieFavoriteAsync(int tmdbId, bool isFavorite)
    {
        await SetAuthHeaderAsync();
        HttpResponseMessage response;
        if (isFavorite)
            response = await _http.DeleteAsync($"api/favorites/movie/{tmdbId}");
        else
            response = await _http.PostAsync($"api/favorites/movie/{tmdbId}", null);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> ToggleSerieFavoriteAsync(int tmdbId, bool isFavorite)
    {
        await SetAuthHeaderAsync();
        HttpResponseMessage response;
        if (isFavorite)
            response = await _http.DeleteAsync($"api/favorites/serie/{tmdbId}");
        else
            response = await _http.PostAsync($"api/favorites/serie/{tmdbId}", null);
        return response.IsSuccessStatusCode;
    }

    public async Task<List<int>> GetFavoriteMovieTmdbIdsAsync()
    {
        await SetAuthHeaderAsync();
        var favorites = await _http.GetFromJsonAsync<List<FavoriteItemDTO>>("api/favorites");
        return favorites?
            .Where(f => f.MovieTmdbId.HasValue)
            .Select(f => f.MovieTmdbId!.Value)  // ← .Value para convertir int? a int
            .ToList() ?? [];
    }

    public async Task<List<int>> GetFavoriteSerieTmdbIdsAsync()
    {
        await SetAuthHeaderAsync();
        var favorites = await _http.GetFromJsonAsync<List<FavoriteItemDTO>>("api/favorites");
        return favorites?
            .Where(f => f.SerieTmdbId.HasValue)
            .Select(f => f.SerieTmdbId!.Value)  // ← .Value para convertir int? a int
            .ToList() ?? [];
    }


    public async Task<(List<FrontMovie> movies, List<TvSeries> series)> GetFavoritesWithDetailsAsync(TmdbService tmdbService)
    {
        await SetAuthHeaderAsync();
        var favorites = await _http.GetFromJsonAsync<List<FavoriteItemDTO>>("api/favorites") ?? [];

        var movieTasks = favorites
             .Where(f => f.MovieTmdbId.HasValue)
             .Select(f => tmdbService.GetMovieDetails(f.MovieTmdbId!.Value));

        var serieTasks = favorites
            .Where(f => f.SerieTmdbId.HasValue)
            .Select(f => tmdbService.GetSerieDetails(f.SerieTmdbId!.Value));

        var movies = (await Task.WhenAll(movieTasks)).ToList();
        var series = (await Task.WhenAll(serieTasks)).ToList();

        return (movies, series);
    }
}

public class FavoriteItemDTO
{
    public int Id { get; set; }
    public int? MovieId { get; set; }
    public int? MovieTmdbId { get; set; }
    public int? SerieId { get; set; }
    public int? SerieTmdbId { get; set; }
    public DateTime AddedAt { get; set; }
}