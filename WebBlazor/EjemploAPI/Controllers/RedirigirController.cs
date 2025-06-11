using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EjemploAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedirigirController : ControllerBase
    {
        [HttpGet("Redirigir")]
        async public Task<IActionResult> RedirigirAsync(string url)
        {
            return Redirect(url);
        }
    }
}
