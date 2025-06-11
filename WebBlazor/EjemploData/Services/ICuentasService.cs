using EjemploData.Models;

namespace EjemploData.Services;

public interface ICuentasService
{
    public Task<List<UsuarioModel>> GetAll();

    public Task<UsuarioModel> GetByNombre(string nombre);

    public Task CrearNuevo(UsuarioModel objeto);

    public Task<List<UsuarioRolModel>> GetRolesByUsuario(string nombreUsuario);

    public Task Actualizar(UsuarioModel objeto);

    public Task Eliminar(string nombre);
}
