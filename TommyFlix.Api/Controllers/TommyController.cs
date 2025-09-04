using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TommyFlix.Api.Data;
using TommyFlix.Shared.DTOs;
using TommyFlix.Shared.Entities;

namespace TommyFlix.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TommyController(DataContext dataContext) : ControllerBase
{
    private readonly DataContext _dataContext = dataContext;

    // GET: api/Tommy/ReleaseSeriesDateToday
    [HttpGet("ReleaseSeriesDateToday")]
    public async Task<PagedResponse<ViewSeriesDTO>> ReleaseSeriesDateToday(int page = 1, int pageSize = 8, CancellationToken cancellationToken = default)
    {
        // Consulta base con filtro “antes de hoy”
        var query = _dataContext.Series.AsNoTracking().Where(s => s.ReleaseDate < DateTime.UtcNow.Date);
        // 1. Total de registros
        var totalCount = await query.CountAsync(cancellationToken);
        // 2. Paginación y proyección
        var items = await query.OrderBy(s => s.Title).Skip((page - 1) * pageSize).Take(pageSize)
            .Select(s => new ViewSeriesDTO
            {
                Id = s.Id,
                Title = s.Title,
                PosterUrl = s.PosterUrl,
                ReleaseDate = s.ReleaseDate,
                Tags = s.Tags!.Select(t => t.Id).ToList(),
                Genders = s.Genders!.Select(g => g.Id).ToList(),
            })
            .ToListAsync(cancellationToken);

        // 3. Devolver items y metadatos
        return new PagedResponse<ViewSeriesDTO>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }

    // GET: api/Tommy/ReleaseMoviesDateToday
    [HttpGet("ReleaseMoviesDateToday")]
    public async Task<PagedResponse<ViewMovieDTO>> ReleaseMoviesDateToday(int page = 1, int pageSize = 8, CancellationToken cancellationToken = default)
    {
        // Consulta base con filtro “antes de hoy”
        var query = _dataContext.Movies.AsNoTracking().Where(s => s.ReleaseDate < DateTime.UtcNow.Date);
        // 1. Total de registros
        var totalCount = await query.CountAsync(cancellationToken);
        // 2. Paginación y proyección
        var items = await query.OrderBy(s => s.Title).Skip((page - 1) * pageSize).Take(pageSize)
            .Select(s => new ViewMovieDTO
            {
                Id = s.Id,
                Title = s.Title,
                PosterUrl = s.PosterUrl,
                ReleaseDate = s.ReleaseDate,
                Duration = s.Duration,
                Tags = s.Tags!.Select(t => t.Id).ToList(),
                Genders = s.Genders!.Select(g => g.Id).ToList(),
            })
            .ToListAsync(cancellationToken);

        // 3. Devolver items y metadatos
        return new PagedResponse<ViewMovieDTO>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }

    // GET: api/Tommy/ShowMovies
    [HttpGet("ShowMovies")]
    public async Task<PagedResponse<ViewMovieDTO>> ShowMovies(int page = 1, int pageSize = 8, CancellationToken cancellationToken = default)
    {
        // Consulta base con filtro “antes de hoy”
        var query = _dataContext.Movies.AsNoTracking();
        // 1. Total de registros
        var totalCount = await query.CountAsync(cancellationToken);
        // 2. Paginación y proyección
        var items = await query.OrderBy(s => s.Title).Skip((page - 1) * pageSize).Take(pageSize)
            .Select(s => new ViewMovieDTO
            {
                Id = s.Id,
                Title = s.Title,
                PosterUrl = s.PosterUrl,
                ReleaseDate = s.ReleaseDate,
                Duration = s.Duration,
                Tags = s.Tags!.Select(t => t.Id).ToList(),
                Genders = s.Genders!.Select(g => g.Id).ToList(),
            })
            .ToListAsync(cancellationToken);

        // 3. Devolver items y metadatos
        return new PagedResponse<ViewMovieDTO>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }

    // GET: api/Tommy/ShowSeries
    [HttpGet("ShowSeries")]
    public async Task<PagedResponse<ViewSeriesDTO>> ShowSeries(int page = 1, int pageSize = 8, CancellationToken cancellationToken = default)
    {
        // Consulta base con filtro “antes de hoy”
        var query = _dataContext.Series.AsNoTracking();
        // 1. Total de registros
        var totalCount = await query.CountAsync(cancellationToken);
        // 2. Paginación y proyección
        var items = await query.OrderBy(s => s.Title).Skip((page - 1) * pageSize).Take(pageSize)
            .Select(s => new ViewSeriesDTO
            {
                Id = s.Id,
                Title = s.Title,
                PosterUrl = s.PosterUrl,
                ReleaseDate = s.ReleaseDate,
                Tags = s.Tags!.Select(t => t.Id).ToList(),
                Genders = s.Genders!.Select(g => g.Id).ToList(),
            })
            .ToListAsync(cancellationToken);

        // 3. Devolver items y metadatos
        return new PagedResponse<ViewSeriesDTO>
        {
            Items = items,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }

    // GET: api/Tommy/ViewMovie/#
    [HttpGet("ViewMovie/{movieId:int}")]
    public async Task<ActionResult<Movie>> ViewMovie(int movieId)
    {
        Movie? movie = await _dataContext.Movies
            .Include(m => m.Tags)
            .Include(m => m.Genders)
            .Include(m => m.Cast)
            .Include(m => m.Ratings)
            .Include(m => m.WatchHistories)
            .FirstOrDefaultAsync(m => m.Id == movieId);
            
        if (movie == null) return NotFound();

        return movie;
    }

    // GET: api/Tommy/ViewSerie/#
    [HttpGet("ViewSerie/{serieId:int}")]
    public async Task<ActionResult<Serie>> ViewSerie(int serieId)
    {
        Serie? serie = await _dataContext.Series
            .Include(m => m.Tags)
            .Include(m => m.Genders)
            .Include(m => m.Cast)
            .Include(m => m.Ratings)
            .Include(m => m.WatchHistories)
            .FirstOrDefaultAsync(m => m.Id == serieId);
            
        if (serie == null) return NotFound();

        return serie;
    }


}
