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
    public class CompanySizeRepository : RepositoryBase<DBGIVENContext, CompanySize , CompanySizeModel>, ICompanySizeRepository    
    {                                                
        private IMapper _mapper;                  
        public CompanySizeRepository(DBGIVENContext dbContext, IMapper mapper) : base(dbContext, mapper)   
        {                                      
            _mapper = mapper;             
        }
        public async Task AddSaveCompanySizeAsync(CompanySizeModel CompanySize)
        {
            Add(CompanySize);
            await SaveChangesAsync();
        }

        public async Task UpdateSaveCompanySizeAsync(CompanySizeModel CompanySize)
        {
            Update(CompanySize);
            await SaveChangesAsync();
        }

        public async Task DeleteSaveCompanySizeAsync(CompanySizeModel CompanySize)
        {
            Delete(CompanySize);
            await SaveChangesAsync();
        }
        public async Task<IEnumerable<CompanySizeModel>> GetAllCompanySizesAsync()
        {
            return await GetAll()
                .OrderBy(s => s.DisplayOrder)
                .ToListAsync();
        }    
        public async Task<CompanySizeModel> GetCompanySizeByIdAsync(Guid CompanySizeid)
        {
            return await Get(s => s.Id.Equals(CompanySizeid)).OrderBy(ss => ss.DisplayOrder).SingleOrDefaultAsync();
        }
        public async Task<CompanySizeModel> GetCompanySizeByNameAsync(string name)
        {
            return await Get(s => s.Size.Equals(name)).SingleOrDefaultAsync();
        } 
    }
}
