using Microsoft.AspNetCore.Mvc;

namespace TommyFlix.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoviesController(HttpClient httpClient, IConfiguration config) : ControllerBase
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly string _apiKey = config["TmdbConfig:ApiKey"];


    [HttpGet("popular")]
    public async Task<IActionResult> GetPopularMovies()
    {
        // El backend construye la URL con la API Key oculta
        var url = $"movie/popular?api_key={_apiKey}&language=es-ES&page=1";
        var response = await _httpClient.GetAsync($"https://api.themoviedb.org{url}");

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return Content(content, "application/json");
        }
        return StatusCode((int)response.StatusCode);
    }
}