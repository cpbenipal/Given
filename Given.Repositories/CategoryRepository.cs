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
    public class CategoryRepository : RepositoryBase<DBGIVENContext, Category , CategoryModel>, ICategoryRepository    
    {                                              
        public CategoryRepository(DBGIVENContext dbContext, IMapper mapper) : base(dbContext, mapper)   
        {                     

        }
        public async Task AddSaveCategoryAsync(CategoryModel Category)
        {
            Add(Category);
            await SaveChangesAsync();
        }

        public async Task UpdateSaveCategoryAsync(CategoryModel Category)
        {
            Update(Category);
            await SaveChangesAsync();
        }

        public async Task DeleteSaveCategoryAsync(CategoryModel Category)
        {
            Delete(Category);
            await SaveChangesAsync();
        }
        public async Task<IEnumerable<CategoryModel>> GetAllCategorysAsync()
        {
            return await GetAll()
                .OrderBy(s => s.Name)
                .ToListAsync();
        }    
        public async Task<CategoryModel> GetCategoryByIdAsync(Guid Categoryid)
        {
            return await Get(s => s.Id.Equals(Categoryid)).SingleOrDefaultAsync();
        }
        public async Task<CategoryModel> GetCategoryByNameAsync(string name)
        {
            return await Get(s => s.Name.Equals(name)).SingleOrDefaultAsync();
        } 
    }
}
