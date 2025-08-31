using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TommyFlix.Api.Data;
using TommyFlix.Shared.Entities;

namespace TommyFlix.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MoviesController(DataContext dataContext) : ControllerBase
{
    private readonly DataContext _dataContext = dataContext;

    // GET: api/Movies
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
    {
        return await _dataContext.Movies.ToListAsync();
    }

    // GET: api/Movies/#
    [HttpGet("{id}")]
    public async Task<ActionResult<Movie>> GetMovie(int id)
    {
        var movie = await _dataContext.Movies.FindAsync(id);

        if (movie == null)
        {
            return NotFound();
        }

        return movie;
    }

    // PUT: api/Movies/#
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMovie(Movie movie)
    {
        _dataContext.Entry(movie).State = EntityState.Modified;

        try
        {
            await _dataContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {

        }

        return NoContent();
    }

    // POST: api/Movies
    [HttpPost]
    public async Task<ActionResult<Movie>> PostMovie(Movie movie)
    {
        _dataContext.Movies.Add(movie);
        await _dataContext.SaveChangesAsync();

        return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
    }

    // DELETE: api/Movies/#
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMovie(int id)
    {
        var movie = await _dataContext.Movies.FindAsync(id);
        if (movie == null)
        {
            return NotFound();
        }

        _dataContext.Movies.Remove(movie);
        await _dataContext.SaveChangesAsync();

        return NoContent();
    }

}
