
using EjemploAPI.Models;

namespace EjemploAPI.Services;

public class PersonasService
{
    static List<PersonaDTO> personaDTOs = new List<PersonaDTO>();
    internal void CrearNuevo(PersonaDTO persona)
    {
        personaDTOs.Add(persona);   
    }

    internal List<PersonaDTO> GetAll()
    {
        return personaDTOs;
    }

    internal PersonaDTO GetById(int id)
    {
        return personaDTOs.Where(p=>p.Id==id).FirstOrDefault();
    }
}
