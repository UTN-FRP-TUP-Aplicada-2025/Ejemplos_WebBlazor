using EjemploData.Models;
using EjemploData.DALs.MSSDALs;

namespace EjemploData.Services;

public class RolesService
{
    readonly private RolesMSSDAL _rolesDao;

    public RolesService(RolesMSSDAL rolesDao)
    {
        _rolesDao = rolesDao;
    }

    async public Task<List<RolModel>> GetAll()
    {
        return await _rolesDao.GetAll();
    }
}
