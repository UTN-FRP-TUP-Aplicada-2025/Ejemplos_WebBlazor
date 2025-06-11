using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EjemploAPI.Models;

public class PersonaDTO
{
    public int Id { get; set; }

    public int DNI { get; set; }

    public string Nombre { get; set; }


    public DateTime? FechaNacimiento { get; set; }
}
