using Given.DataContext.Entities;
using Given.Models;
using Given.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Given.Repositories.Generic
{
    public interface IDesignationRepository
    {
        Task<PagedCollection<DesignationModel>> GetAllAsync(QueryParam contactListParam); 
        Task<DesignationModel> GetByIdAsync(Guid Designationid);
        Task<IEnumerable<DesignationDDModel>> GetByNameAsync(Guid userid); 
        Task<string> SaveAsync(DesignationModel Designation);      
        Task DeleteSaveAsync(DesignationModel Designation);        
    }
}