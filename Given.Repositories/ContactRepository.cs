using AutoMapper;
using Given.DataContext.Entities;
using Given.Models;
using Given.Models.Helpers;
using Given.Repositories.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Given.Repositories
{
    public class ContactRepository : RepositoryBase<DBGIVENContext, Contacts, ContactModel>, IContactRepository
    {
        private IMapper _mapper;    
        public ContactRepository(DBGIVENContext dbContext, IMapper mapper) : base(dbContext, mapper)
        {
            _mapper = mapper;   
        }

        public async Task<string> SaveAsync(ContactModel contact)
        {
            try
            {
                var entities = _mapper.Map<ContactModel, Contacts>(contact);

                if (contact.IsAdd == true)
                {
                    entities.UpdatedBy = contact.Id;
                    entities.UpdatedOn = DateTime.Now;
                    entities.Id = Guid.NewGuid();
                    entities.CreatedBy = contact.Id;
                    entities.CreatedOn = DateTime.Now;
                    entities.IsActive = true;
                    AddWithoutMapping(entities);
                }
                else
                {
                    var entity = databaseContext.Contacts.Where(x => x.Id == contact.Id)
                        .Select(m => new Contacts
                        {
                            Prefix = contact.Prefix,
                            FirstName = contact.FirstName,
                            MiddleName = contact.MiddleName,
                            LastName = contact.LastName,
                            PrimaryEmail = contact.PrimaryEmail,
                            SecondaryEmail = contact.SecondaryEmail,
                            Mobile = contact.Mobile,
                            Phone = contact.Phone,
                            Gender = contact.Gender,
                            Photo = null,
                            Address1 = contact.Address1,
                            Address2 = contact.Address2,
                            City = contact.City,
                            ZipCode = contact.ZipCode,
                            State = contact.State,
                            Birthday = contact.Birthday,
                            CreatedBy = m.CreatedBy,
                            CreatedOn = m.CreatedOn,
                            Id = m.Id,
                            IsActive = contact.IsActive,
                            UpdatedBy = contact.UserId,
                            UpdatedOn = DateTime.Now,
                            UserId = contact.UserId
                        }).FirstOrDefault();


                    if (entity != null)
                    {
                        UpdateWithoutMapping(entity);
                    }
                    else
                    {
                        return "403";
                    }

                }
                await SaveChangesAsync();
                return "200";
            }
            catch (Exception x)
            {
                return "404";
            }
        }

        public async Task<string> DeleteSaveAsync(Guid contactId)
        {
            try
            {
                var contact = await Get(x => x.Id == contactId).FirstOrDefaultAsync();
                if (contact != null)
                {
                    Delete(contact);
                    await SaveChangesAsync();
                    return "200";
                }
                else
                    return "400";
            }
            catch (Exception x)
            {
                return "404";
            }
        }

        public async Task<PagedCollection<ContactModel>> GetAllAsync(QueryParam param)
        {
            Guid guid = new Guid(param.Query);
            var contacts = GetAll(x => x.UserId == guid);
            var propertyInfo = typeof(ContactModel).GetProperty(param.OrderBy);
            var result = await GetAllWithPaging(param.PageIndex, param.PageSize, propertyInfo, param.OrderDir, contacts);
            return result;
        }

        public async Task<PagedCollection<ContactModel>> GetAllBySuperAdminAsync(QueryParam param)
        {
            Guid guid = new Guid(param.Query);
            var contacts = GetAll(x => x.UserId == guid);
            var propertyInfo = typeof(ContactModel).GetProperty(param.OrderBy);
            var result = await GetAllWithPaging(param.PageIndex, param.PageSize, propertyInfo, param.OrderDir, contacts);
            return result;
        }

        public async Task<ContactModel> GetByIdAsync(Guid id)
        {
            return await Get(s => s.Id.Equals(id)).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<ContactDropdownModel>> GetByNameAsync(Guid userid)
        {
            var entity = await databaseContext.Contacts.Where(x => x.UserId == userid && x.IsActive).Select( x=> new ContactDropdownModel { Id = x.Id, FirstName = x.FirstName  }).OrderBy(o=>o.FirstName).ToListAsync();
            return entity;
        }
    }
}
