using EjemploWeb.Models;

namespace EjemploWeb.Services
{
    public class PersonasService
    {
        List<PersonaModel> personaModels = new List<PersonaModel>();

        async public Task<List<PersonaModel>> GetAll()
        {
            return personaModels;
        }

        async public Task<PersonaModel> GetOneAsync(int? id)
        {
            return null;
        }

        async public Task<PersonaModel> InsertAsync(PersonaModel _personaModel)
        {
            return null;
        }
   
        async public Task<List<PersonaModel>> GetListByActivoAsync(bool activo)
        {
            return null;
        }

        async public Task UpdateAsync(PersonaModel _personaModel)
        {
        }

        async public Task DeleteAsync(int? id)
        { 
        }


    }
}
