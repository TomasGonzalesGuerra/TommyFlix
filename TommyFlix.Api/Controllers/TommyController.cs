using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TommyFlix.Api.Data;
using TommyFlix.Shared.DTOs;

namespace TommyFlix.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TommyController(DataContext dataContext) : ControllerBase
{
    private readonly DataContext _dataContext = dataContext;

    // GET: api/Tommy/ReleaseDateToday
    [HttpGet("ReleaseDateToday")]
    public async Task<PagedResponse<ViewSeriesDTO>> ReleaseDateToday(int page = 1, int pageSize = 20, CancellationToken cancellationToken = default)
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
                ReleaseDate = s.ReleaseDate,
                PosterUrl = s.PosterUrl,
                Genders = s.Genders!.Select(g => g.Id).ToList(),
                Tags = s.Tags!.Select(t => t.Id).ToList()
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


}
