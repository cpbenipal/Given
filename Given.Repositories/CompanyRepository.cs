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
    public class CompanyRepository : RepositoryBase<DBGIVENContext, Company, CompanyModel>, ICompanyRepository
    {
        private IMapper _mapper;
        public CompanyRepository(DBGIVENContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _mapper = mapper;
        }
        public async Task AddSaveCompanyAsync(CompanyModel Company)
        {
            Add(Company);
            await SaveChangesAsync();
        }

        public async Task<string> UpdateSaveCompanyAsync(UpdateCompanyModel model)
        {
            try
            {
                //var company = await GetCompanyByIdAsync(model.CompanyId);
                var company = databaseContext.Company.Join(databaseContext.User, u => u.CompanyId, uir => uir.CompanyId, (u, uir) => new { u, uir })
                    .Where(x => x.uir.Id == model.UserId)
                    .Select(m => new Company
                    {
                        CompanyId = model.CompanyId,
                        UpdatedOn = DateTime.Now,
                        CreatedOn = m.u.CreatedOn,
                        CompanySizeId = model.CompanySize.Id,
                        CompanyName = model.CompanyName,
                        Phone = model.Phone,
                        StreetLine1 = model.StreetLine1,
                        StreetLine2 = model.StreetLine2,
                        City = model.City,
                        State = model.State,
                        Zip = model.Zip,
                        Website = model.Website,
                        Description = model.Description,
                        Fax = model.Fax
                    }).FirstOrDefault();
                if (company != null)
                {
                    UpdateWithoutMapping(company);
                    await SaveChangesAsync();
                    return "200";
                }
                else
                {
                    return "400";
                }
            }
            catch (Exception e)
            {
                return "404";
            }
        }

        public async Task DeleteSaveCompanyAsync(CompanyModel Company)
        {
            Delete(Company);
            await SaveChangesAsync();
        }
        public async Task<IEnumerable<CompanyModel>> GetAllCompanysAsync()
        {
            return await GetAll()
                .OrderBy(s => s.CompanyName)
                .ToListAsync();
        }
        public async Task<CompanyModel> GetCompanyByIdAsync(Guid Companyid)
        {
            var company = await Get(s => s.CompanyId.Equals(Companyid)).SingleOrDefaultAsync();
            return company;
        }
        public async Task<CompanyModel> GetCompanyByNameAsync(string name)
        {
            return await Get(s => s.CompanyName.Equals(name)).SingleOrDefaultAsync();
        }
    }
}
