using Microsoft.AspNetCore.Mvc;

namespace TommyFlix.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HiController : ControllerBase
{
    // GET: api/Hi
    [HttpGet]
    public async Task<ActionResult<string>> GetAdminDashboardAsync()
    {
        return Ok($"Hola soy Yop");
    }
}
