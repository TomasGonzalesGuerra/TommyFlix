using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TommyFlix.Api.Data;
using TommyFlix.Shared.Entities;

namespace TommyFlix.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class FavoritesController(DataContext context) : ControllerBase
{
    private readonly DataContext _context = context;

    private string GetUserId() => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

    // GET: api/favorites
    [HttpGet]
    public async Task<ActionResult> GetFavorites()
    {
        var userId = GetUserId();
        var favorites = await _context.Favorites
            .Where(f => f.UserId == userId)
            .Include(f => f.Movie)
            .Include(f => f.Serie)
            .Select(f => new
            {
                f.Id,
                f.AddedAt,
                MovieTmdbId = f.Movie != null ? f.Movie.TmdbId : (int?)null,
                SerieTmdbId = f.Serie != null ? f.Serie.TmdbId : (int?)null,
            })
            .ToListAsync();
        return Ok(favorites);
    }

    // GET: api/favorites/movie/{tmdbId}
    [HttpGet("movie/{tmdbId}")]
    public async Task<ActionResult<bool>> IsMovieFavorite(int tmdbId)
    {
        var userId = GetUserId();
        var exists = await _context.Favorites
            .AnyAsync(f => f.UserId == userId && f.Movie!.TmdbId == tmdbId);
        return Ok(exists);
    }

    // GET: api/favorites/serie/{tmdbId}
    [HttpGet("serie/{tmdbId}")]
    public async Task<ActionResult<bool>> IsSerieFavorite(int tmdbId)
    {
        var userId = GetUserId();
        var exists = await _context.Favorites
            .AnyAsync(f => f.UserId == userId && f.Serie!.TmdbId == tmdbId);
        return Ok(exists);
    }

    // POST: api/favorites/movie/{tmdbId}
    [HttpPost("movie/{tmdbId}")]
    public async Task<ActionResult> AddMovieFavorite(int tmdbId)
    {
        var userId = GetUserId();

        // Buscar o crear la Movie en nuestra BD
        var movie = await _context.Movies.FirstOrDefaultAsync(m => m.TmdbId == tmdbId);
        if (movie == null)
        {
            movie = new Movie { TmdbId = tmdbId };
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
        }

        // Verificar que no exista ya el favorito
        var exists = await _context.Favorites
            .AnyAsync(f => f.UserId == userId && f.MovieId == movie.Id);
        if (exists) return BadRequest("Ya está en favoritos.");

        _context.Favorites.Add(new UserFavorite
        {
            UserId = userId,
            MovieId = movie.Id,
            AddedAt = DateTime.UtcNow
        });

        await _context.SaveChangesAsync();
        return Ok();
    }

    // POST: api/favorites/serie/{tmdbId}
    [HttpPost("serie/{tmdbId}")]
    public async Task<ActionResult> AddSerieFavorite(int tmdbId)
    {
        var userId = GetUserId();

        var serie = await _context.Series.FirstOrDefaultAsync(s => s.TmdbId == tmdbId);
        if (serie == null)
        {
            serie = new Serie { TmdbId = tmdbId };
            _context.Series.Add(serie);
            await _context.SaveChangesAsync();
        }

        var exists = await _context.Favorites
            .AnyAsync(f => f.UserId == userId && f.SerieId == serie.Id);
        if (exists) return BadRequest("Ya está en favoritos.");

        _context.Favorites.Add(new UserFavorite
        {
            UserId = userId,
            SerieId = serie.Id,
            AddedAt = DateTime.UtcNow
        });

        await _context.SaveChangesAsync();
        return Ok();
    }

    // DELETE: api/favorites/movie/{tmdbId}
    [HttpDelete("movie/{tmdbId}")]
    public async Task<ActionResult> RemoveMovieFavorite(int tmdbId)
    {
        var userId = GetUserId();
        var favorite = await _context.Favorites
            .FirstOrDefaultAsync(f => f.UserId == userId && f.Movie!.TmdbId == tmdbId);
        if (favorite == null) return NotFound();

        _context.Favorites.Remove(favorite);
        await _context.SaveChangesAsync();
        return Ok();
    }

    // DELETE: api/favorites/serie/{tmdbId}
    [HttpDelete("serie/{tmdbId}")]
    public async Task<ActionResult> RemoveSerieFavorite(int tmdbId)
    {
        var userId = GetUserId();
        var favorite = await _context.Favorites
            .FirstOrDefaultAsync(f => f.UserId == userId && f.Serie!.TmdbId == tmdbId);
        if (favorite == null) return NotFound();

        _context.Favorites.Remove(favorite);
        await _context.SaveChangesAsync();
        return Ok();
    }
}