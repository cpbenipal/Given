using AutoMapper;
using Given.DataContext.Entities;
using Given.Repositories.Generic;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using Given.Models.Helpers;
using Microsoft.Data.SqlClient;

namespace Given.Repositories
{
    public class GiftRepository : RepositoryBase<DBGIVENContext, Gift, GiftModel>, IGiftRepository
    {
        private IMapper _mapper;
        public GiftRepository(DBGIVENContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _mapper = mapper;
        }
        public async Task<string> SaveAsync(GiftModel Gift)
        {
            if (Gift.IsAdd)
            {
                Gift.UpdatedOn = DateTime.Now;
                Gift.UpdatedBy = Gift.UserId;
                Gift.CreatedBy = Gift.UserId;
                Gift.CreatedOn = DateTime.Now;
                Add(Gift);
            }
            else
            {
                var entity = await GetByIdAsync(Gift.Id);
                entity.Description = Gift.Description;
                entity.Amount = Gift.Amount;  
                entity.ContactId = Gift.ContactId;
                entity.DesignationId = Gift.DesignationId;
                entity.UpdatedOn = DateTime.Now;
                entity.UpdatedBy = Gift.UserId;
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

        public async Task DeleteSaveAsync(GiftModel Gift)
        {
            Delete(Gift);
            await SaveChangesAsync();
        }
        public async Task<PagedCollection<GiftListModel>> GetAllAsync(QueryParam param)
        {
            Guid guid = new Guid(param.Query);
            var propertyInfo = typeof(GiftModel).GetProperty(param.OrderBy);
            var gifts = databaseContext.Gift.Include(xx=>xx.Contact).Include(d=>d.Designation).Where(x => x.UserId == guid);
            if (param.OrderDir == SorderOrder.ASC)
            {
                gifts = gifts.OrderBy(c => propertyInfo.Name);
            }
            else
            {
                gifts = gifts.OrderByDescending(c => propertyInfo.Name);
            }
            var result = await GetPagingWithoutMapping(param.PageIndex, param.PageSize, propertyInfo, param.OrderDir, gifts);


            var giftlist = (from f in result.Items         
                            select new GiftListModel
                            {
                                Id = f.Id,
                                Amount = f.Amount,                                 
                                Description = f.Description,
                                GiftDate = f.GiftDate,
                                CreatedOn = f.CreatedOn,
                                Contact = f.Contact.FirstName,
                                Designation = f.Designation.Name,
                                UserId = f.UserId,
                                ContactId = f.ContactId,
                                DesignationId = f.DesignationId
                            }).ToList();


            return new PagedCollection<GiftListModel>
            {
                Page = result.Page,
                TotalCount = result.TotalCount,
                TotalPages = result.TotalPages,
                Items = giftlist
            };
        }
        public async Task<GiftModel> GetByIdAsync(Guid Giftid)
        {
            return await Get(s => s.Id.Equals(Giftid)).SingleOrDefaultAsync();
        }
        public async Task<IEnumerable<GiftListModel>> GetByNameAsync(Guid userid)
        {
            var entity = await databaseContext.Gift.Where(x => x.UserId == userid).Select(x => new GiftListModel { Id = x.Id, Description = x.Description }).OrderBy(o => o.Description).ToListAsync();
            return entity;
        }
    }
}
