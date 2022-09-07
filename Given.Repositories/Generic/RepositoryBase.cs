using System;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Collections.Generic;
using Given.Models.Helpers;
using System.Reflection;

namespace Given.Repositories.Generic
{
    public abstract class RepositoryBase<TC, TS, TD> : IRepositoryBase<TS, TD>
        where TC : DbContext where TS : class where TD : class
    {
        protected readonly IMapper mapper;
        protected readonly TC databaseContext;
        private void LogInfo(string msg = "")
        {
            ILog logger = new LogNLog();
            logger.Information("Information is logged");
            logger.Warning("Warning is logged");
            logger.Debug("Debug log is logged");
            logger.Error(msg);
        }
        protected RepositoryBase(TC databaseContext, IMapper mapper)
        {
            this.databaseContext = databaseContext;
            this.mapper = mapper;
        }

        public IQueryable<TS> GetWithoutMapping(Expression<Func<TS, bool>> expression)
        {
            return databaseContext.Set<TS>().Where(expression);
        }

        public IQueryable<TS> GetAllWithoutMapping()
        {
            return databaseContext.Set<TS>();
        }

        public void UpdateWithoutMapping(TS entity)
        {
            databaseContext.Set<TS>().Update(entity);
        }

        public void AddWithoutMapping(TS entity)
        {
            databaseContext.Set<TS>().Add(entity);
        }

        public void DeleteWithoutMapping(TS entity)
        {
            databaseContext.Set<TS>().Remove(entity);
        }

        public IQueryable<TD> Get(Expression<Func<TS, bool>> expression)
        {
            return mapper.ProjectTo<TD>(databaseContext.Set<TS>().Where(expression));
        }

        public IQueryable<TD> GetAll()
        {
            return mapper.ProjectTo<TD>(databaseContext.Set<TS>());
        }

        public IQueryable<TD> GetAll(Expression<Func<TS, bool>> expression)
        {
            return mapper.ProjectTo<TD>(databaseContext.Set<TS>().Where(expression));
        }

        public void Update(TD entity)
        {
            databaseContext.Set<TS>().Update(mapper.Map<TS>(entity));
        }

        public void Add(TD entity)
        {
            databaseContext.Set<TS>().Add(mapper.Map<TS>(entity));
        }

        public void Delete(TD entity)
        {
            databaseContext.Set<TS>().Remove(mapper.Map<TS>(entity));
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await databaseContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                LogInfo(ex.InnerException.Message);
            }
        }         
        public async Task<PagedCollection<TS>> GetPagingWithoutMapping(int? page, int? pageSize, PropertyInfo SortColumn, SorderOrder SortDir, IQueryable<TS> entities)
        {
            var currPage = page.GetValueOrDefault(0);
            var currPageSize = pageSize.GetValueOrDefault(100);

            var paged = (from f in entities
                         select f).Skip(currPage * currPageSize).Take(currPageSize);

            //if (SortDir == SorderOrder.ASC)
            //{
            //    paged = paged.OrderBy(c => SortColumn.Name);
            //}
            //else
            //{
            //    paged = paged.OrderByDescending(c => SortColumn.Name);
            //}

            var totalCount = await entities.CountAsync();

            return new PagedCollection<TS>()
            {
                Page = currPage,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((decimal)totalCount / currPageSize),
                Items = paged
            };
        }
        public async Task<PagedCollection<TD>> GetAllWithPaging(int? page, int? pageSize, PropertyInfo SortColumn, SorderOrder SortDir, IQueryable<TD> entities)
        {
            var currPage = page.GetValueOrDefault(0);
            var currPageSize = pageSize.GetValueOrDefault(100);

            var paged = (from f in entities
                         select f).Skip(currPage * currPageSize).Take(currPageSize);

            //if (SortDir == SorderOrder.ASC)
            //{
            //    paged = paged.OrderBy(c => SortColumn.Name);
            //}
            //else
            //{
            //    paged = paged.OrderByDescending(c => SortColumn.Name);
            //}

            var totalCount = await entities.CountAsync();

            return new PagedCollection<TD>()
            {
                Page = currPage,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling((decimal)totalCount / currPageSize),
                Items = paged
            };
        }
    }

}
