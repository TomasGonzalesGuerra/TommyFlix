using System.Net.Http.Json;
using TommyFlix.Web.Models;

namespace TommyFlix.Web.Services;

public class TmdbService(IHttpClientFactory httpFactory)
{
    private readonly HttpClient _http = httpFactory.CreateClient("tmdb");

    public async Task<TmdbResponse<FrontMovie>> GetPopularMovies() => 
        await _http.GetFromJsonAsync<TmdbResponse<FrontMovie>>("movie/popular?language=es-ES");

    public async Task<TmdbResponse<FrontMovie>> GetPopularActionMovies() =>
        await _http.GetFromJsonAsync<TmdbResponse<FrontMovie>>("discover/movie?with_genres=28&language=es-ES");

    public async Task<TmdbResponse<TvSeries>> GetPopularSeries() =>
        await _http.GetFromJsonAsync<TmdbResponse<TvSeries>>("tv/popular?language=es-ES");

    public async Task<TmdbResponse<TvSeries>> GetOnTheAirSeries() =>
        await _http.GetFromJsonAsync<TmdbResponse<TvSeries>>("tv/on_the_air?language=es-ES");

    public async Task<FrontMovie> GetMovieDetails(int id) =>
    await _http.GetFromJsonAsync<FrontMovie>($"movie/{id}?language=es-ES&append_to_response=credits");

    public async Task<TvSeries> GetSerieDetails(int id) =>
        await _http.GetFromJsonAsync<TvSeries>($"tv/{id}?language=es-ES&append_to_response=credits");

    public async Task<TmdbResponse<FrontMovie>> SearchMovies(string query) =>
        await _http.GetFromJsonAsync<TmdbResponse<FrontMovie>>($"search/movie?query={Uri.EscapeDataString(query)}&language=es-ES");

    public async Task<TmdbResponse<TvSeries>> SearchSeries(string query) =>
        await _http.GetFromJsonAsync<TmdbResponse<TvSeries>>($"search/tv?query={Uri.EscapeDataString(query)}&language=es-ES");




}
