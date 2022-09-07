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
using Given.Models.Helpers;
using Microsoft.Data.SqlClient;

namespace Given.Repositories
{
    public class DesignationRepository : RepositoryBase<DBGIVENContext, Designation, DesignationModel>, IDesignationRepository
    {
        private IMapper _mapper;
        public DesignationRepository(DBGIVENContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _mapper = mapper;
        }
        public async Task<string> SaveAsync(DesignationModel Designation)
        {      
            if (Designation.IsAdd)
            {
                Designation.UpdatedOn = DateTime.Now;
                Designation.UpdatedBy = Designation.UserId;
                Designation.CreatedBy = Designation.UserId;
                Designation.CreatedOn = DateTime.Now;
                Add(Designation);
            }
            else
            {
                var entity = await GetByIdAsync(Designation.Id);
                entity.Name = Designation.Name;
                entity.Status = Designation.Status;
                entity.CategoryId = Designation.CategoryId;
                entity.UpdatedOn = DateTime.Now;
                entity.UpdatedBy = Designation.UserId;
                Update(entity);
            }
            try
            {
                await SaveChangesAsync();                  
            }
            catch (Exception x)
            {
                SqlException innerException = x.InnerException as SqlException;
                if (innerException != null && (innerException.Number == 2627 || innerException.Number == 2601))
                {
                    return "2601";
                }
                else
                {
                    return "403";
                }
            }
            return "200";
        }

        public async Task DeleteSaveAsync(DesignationModel Designation)
        {
            Delete(Designation);
            await SaveChangesAsync();
        }
        public async Task<PagedCollection<DesignationModel>> GetAllAsync(QueryParam param)
        {
            Guid guid = new Guid(param.Query);
            var propertyInfo = typeof(DesignationModel).GetProperty(param.OrderBy);
            var contacts = GetAll(x => x.UserId == guid);
            if (param.OrderDir == SorderOrder.ASC)
            {
                contacts = contacts.OrderBy(c => propertyInfo.Name);
            }
            else
            {
                contacts = contacts.OrderByDescending(c => propertyInfo.Name);
            }
            var result = await GetAllWithPaging(param.PageIndex, param.PageSize, propertyInfo, param.OrderDir, contacts);
            return result;
        }
        public async Task<DesignationModel> GetByIdAsync(Guid Designationid)
        {
            return await Get(s => s.Id.Equals(Designationid)).SingleOrDefaultAsync();
        }
        public async Task<IEnumerable<DesignationDDModel>> GetByNameAsync(Guid userid)
        {
            var entity = await databaseContext.Designation.Where(x => x.UserId == userid && x.Status == true).Select(x => new DesignationDDModel { Id = x.Id, Name = x.Name }).OrderBy(o => o.Name).ToListAsync();
            return entity;
        }
    }
}
