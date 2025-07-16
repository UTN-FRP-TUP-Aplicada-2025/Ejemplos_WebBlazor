using EjemploAPI.Models;
using EjemploAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace EjemploAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PersonasController : ControllerBase
{
    public PersonasService _servicio;

    public PersonasController(PersonasService _servicio)
    { 
        this._servicio = _servicio; 
    }

    // GET: api/<PersonasController>
    [HttpGet]
    public IActionResult Get()
    {
        List<PersonaDTO> personas = _servicio.GetAll();
        if(personas.Count>=0)
            return Ok(personas);
        else
            return NotFound("No hay personas registradas");


        return Ok(_servicio.GetAll());
    }

    // GET api/<PersonasController>/5
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var persona = _servicio.GetById(id);
        return Ok(persona);
    }

    // POST api/<PersonasController>
    [HttpPost]
    public IActionResult Post([FromBody] PersonaDTO persona)
    {
        _servicio.CrearNuevo(persona);
        return RedirectToAction(nameof(Index));
    }
       

    // PUT api/<PersonasController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<PersonasController>/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int? id)
    {
        if (id == null || id <= 0)
            return BadRequest();

        var persona = _servicio.GetById(Convert.ToInt32(id));

        if (persona == null)
            return NotFound();

        return Ok();
    }
}
