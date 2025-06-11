using EjemploAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EjemploAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EjemplosController : ControllerBase
{
    List<EjemploDTO> lista = new List<EjemploDTO>();

    [HttpGet]
    async public Task<IActionResult> Get()
    {
        return Ok(lista);
    }
}
