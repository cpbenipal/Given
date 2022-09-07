using Given.Models;
using Given.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Given.Repositories.Generic
{
    public interface IContactRepository
    {
        Task<PagedCollection<ContactModel>> GetAllBySuperAdminAsync(QueryParam param); 
        Task<PagedCollection<ContactModel>> GetAllAsync(QueryParam param);
        Task<ContactModel> GetByIdAsync(Guid id);
        Task<IEnumerable<ContactDropdownModel>> GetByNameAsync(Guid userid);
        Task<string> SaveAsync(ContactModel contact); 
        Task<string> DeleteSaveAsync(Guid contactId);
    }
}