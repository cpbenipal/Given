using Given.Models.Helpers;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Given.Repositories.Generic
{
    public interface IRepositoryBase<TS,TD> where TS : class where TD : class
    {
        IQueryable<TS> GetWithoutMapping(Expression<Func<TS, bool>> expression);
        IQueryable<TS> GetAllWithoutMapping();
        void UpdateWithoutMapping(TS entity);
        void AddWithoutMapping(TS entity);
        void DeleteWithoutMapping(TS entity);

        IQueryable<TD> Get(Expression<Func<TS, bool>> expression);
        IQueryable<TD> GetAll();
        Task<PagedCollection<TD>> GetAllWithPaging(int? page, int? pageSize, PropertyInfo SortColumn, SorderOrder SortDir, IQueryable<TD> entities);
        IQueryable<TD> GetAll(Expression<Func<TS, bool>> expression);
        void Update(TD entity);
        void Add(TD entity);
        void Delete(TD entity);
        Task SaveChangesAsync();
    }
}
