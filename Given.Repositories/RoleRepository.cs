using AutoMapper;
using Microsoft.Extensions.Options;
using Given.DataContext.Entities;
using Given.Models;
using Given.Repositories.Generic;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using Given.EmailService;

namespace Given.Repositories
{
    public class RoleRepository : RepositoryBase<DBGIVENContext, Role , RoleModel>, IRoleRepository    
    {                                              
        public RoleRepository(DBGIVENContext dbContext, IMapper mapper) : base(dbContext, mapper)   
        {                     

        }
        public async Task AddSaveRoleAsync(RoleModel Role)
        {
            Add(Role);
            await SaveChangesAsync();
        }

        public async Task UpdateSaveRoleAsync(RoleModel Role)
        {
            Update(Role);
            await SaveChangesAsync();
        }

        public async Task DeleteSaveRoleAsync(RoleModel Role)
        {
            Delete(Role);
            await SaveChangesAsync();
        }
        public async Task<IEnumerable<RoleModel>> GetAllRolesAsync()
        {
            return await GetAll()
                .OrderBy(s => s.RoleName)
                .ToListAsync();
        }    
        public async Task<RoleModel> GetRoleByIdAsync(Guid roleid)
        {
            return await Get(s => s.RoleId.Equals(roleid)).SingleOrDefaultAsync();
        }
        public async Task<RoleModel> GetRoleByNameAsync(string name)
        {
            return await Get(s => s.RoleName.Equals(name)).SingleOrDefaultAsync();
        } 
    }
}
