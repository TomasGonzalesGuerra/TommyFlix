using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TommyFlix.Api.Data;
using TommyFlix.Shared.Entities;

namespace TommyFlix.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SeriesController(DataContext dataContext) : ControllerBase
{
    private readonly DataContext _dataContext = dataContext;

    // GET: api/Series
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Serie>>> GetSeries()
    {
        return await _dataContext.Series.ToListAsync();
    }

    // GET: api/Series/#
    [HttpGet("{id}")]
    public async Task<ActionResult<Serie>> GetSerie(int id)
    {
        var serie = await _dataContext.Series.FindAsync(id);

        if (serie == null)
        {
            return NotFound();
        }

        return serie;
    }

    // PUT: api/Series/#
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSerie(Serie serie)
    {
        _dataContext.Entry(serie).State = EntityState.Modified;

        try
        {
            await _dataContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {

        }

        return NoContent();
    }

    // POST: api/Series
    [HttpPost]
    public async Task<ActionResult<Serie>> PostSerie(Serie serie)
    {
        _dataContext.Series.Add(serie);
        await _dataContext.SaveChangesAsync();

        return CreatedAtAction("GetSerie", new { id = serie.Id }, serie);
    }

    // DELETE: api/Series/#
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSerie(int id)
    {
        var serie = await _dataContext.Series.FindAsync(id);
        if (serie == null)
        {
            return NotFound();
        }

        _dataContext.Series.Remove(serie);
        await _dataContext.SaveChangesAsync();

        return NoContent();
    }

}
