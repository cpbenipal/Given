using Given.DataContext.Entities;
using Given.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Given.Repositories.Generic
{
    public interface IRoleRepository
    {
        Task<IEnumerable<RoleModel>> GetAllRolesAsync();
        Task<RoleModel> GetRoleByIdAsync(Guid roleid);
        Task<RoleModel> GetRoleByNameAsync(string name);
        Task AddSaveRoleAsync(RoleModel Role);
        Task UpdateSaveRoleAsync(RoleModel Role);
        Task DeleteSaveRoleAsync(RoleModel Role);
    }
}