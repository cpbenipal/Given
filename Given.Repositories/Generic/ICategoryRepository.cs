using Given.DataContext.Entities;
using Given.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Given.Repositories.Generic
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryModel>> GetAllCategorysAsync();
        Task<CategoryModel> GetCategoryByIdAsync(Guid Categoryid);
        Task<CategoryModel> GetCategoryByNameAsync(string name);
        Task AddSaveCategoryAsync(CategoryModel Category);
        Task UpdateSaveCategoryAsync(CategoryModel Category);
        Task DeleteSaveCategoryAsync(CategoryModel Category);
    }
}