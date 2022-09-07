using Given.DataContext.Entities;
using Given.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Given.Repositories.Generic
{
    public interface ICompanySizeRepository
    {
        Task<IEnumerable<CompanySizeModel>> GetAllCompanySizesAsync();
        Task<CompanySizeModel> GetCompanySizeByIdAsync(Guid CompanySizeid);
        Task<CompanySizeModel> GetCompanySizeByNameAsync(string name);
        Task AddSaveCompanySizeAsync(CompanySizeModel CompanySize);
        Task UpdateSaveCompanySizeAsync(CompanySizeModel CompanySize);
        Task DeleteSaveCompanySizeAsync(CompanySizeModel CompanySize);
    }
}