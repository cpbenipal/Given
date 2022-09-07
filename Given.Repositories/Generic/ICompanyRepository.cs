using Given.DataContext.Entities;
using Given.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Given.Repositories.Generic
{
    public interface ICompanyRepository
    {
        Task<IEnumerable<CompanyModel>> GetAllCompanysAsync();
        Task<CompanyModel> GetCompanyByIdAsync(Guid Companyid);
        Task<CompanyModel> GetCompanyByNameAsync(string name); 
      //  Task AddSaveCompanyAsync(CompanyModel Company);
        Task<string> UpdateSaveCompanyAsync(UpdateCompanyModel Company);
       // Task DeleteSaveCompanyAsync(CompanyModel Company);
    }
}