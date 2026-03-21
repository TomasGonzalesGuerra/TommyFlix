using System.Net.Http.Json;
using TommyFlix.Web.Models;

namespace TommyFlix.Web.Services;

public class TmdbService(HttpClient http, IConfiguration config)
{
    private readonly HttpClient _http = http;
    private readonly IConfiguration _config = config;

    public async Task<MovieResponse> GetPopularActionMovies()
    {
        var url = $"discover/movie?api_key={_config["TmdbConfig:ApiKey"]}" +
                  "&sort_by=popularity.desc" +
                  "&with_genres=28" +
                  "&page=1";

        return await _http.GetFromJsonAsync<MovieResponse>(url);
    }
}