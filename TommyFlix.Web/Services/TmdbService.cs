using System.Net.Http.Json;
using TommyFlix.Web.Models;

namespace TommyFlix.Web.Services;

public class TmdbService(HttpClient http, IConfiguration config)
{
    private readonly HttpClient _http = http;
    private readonly IConfiguration _config = config;

    public async Task<Movie> GetMovieDetails(int id)
    {
        var url = $"movie/{id}?api_key={_config["TmdbConfig:ApiKey"]}&language=es-ES";
        return await _http.GetFromJsonAsync<Movie>(url);
    }

    public async Task<TmdbResponse<TvSeries>> GetPopularSeries()
    {
        var url = $"tv/popular?api_key={_config["TmdbConfig:ApiKey"]}&language=es-ES";
        return await _http.GetFromJsonAsync<TmdbResponse<TvSeries>>(url);
    }

    public async Task<TvSeries> GetSerieDetails(int id)
    {
        var url = $"tv/{id}?api_key={_config["TmdbConfig:ApiKey"]}&language=es-ES";
        return await _http.GetFromJsonAsync<TvSeries>(url);
    }

    public async Task<TmdbResponse<Movie>> GetPopularMovies()
    {
        var url = $"movie/popular?api_key={_config["TmdbConfig:ApiKey"]}&language=es-ES";
        return await _http.GetFromJsonAsync<TmdbResponse<Movie>>(url);
    }

    // Opcional: por género (acción = 28)
    public async Task<TmdbResponse<Movie>> GetPopularActionMovies()
    {
        var url = $"discover/movie?api_key={_config["TmdbConfig:ApiKey"]}&with_genres=28&language=es-ES";
        return await _http.GetFromJsonAsync<TmdbResponse<Movie>>(url);
    }
}

//public Task<TmdbResponse<Movie>> GetPopularMovies()
//    {
//        var url = $"movie/popular?api_key={ApiKey}&language=es-ES";
//        return _http.GetFromJsonAsync<TmdbResponse<Movie>>(url);
//    }

//    public Task<TmdbResponse<Movie>> GetPopularActionMovies()
//    {
//        var url = $"discover/movie?api_key={ApiKey}&with_genres=28&language=es-ES";
//        return _http.GetFromJsonAsync<TmdbResponse<Movie>>(url);
//    }

//    public Task<TmdbResponse<TvSeries>> GetPopularSeries()
//    {
//        var url = $"tv/popular?api_key={ApiKey}&language=es-ES";
//        return _http.GetFromJsonAsync<TmdbResponse<TvSeries>>(url);
//    }

//    public Task<TmdbResponse<TvSeries>> GetOnTheAirSeries()
//    {
//        var url = $"tv/on_the_air?api_key={ApiKey}&language=es-ES";
//        return _http.GetFromJsonAsync<TmdbResponse<TvSeries>>(url);
//    }