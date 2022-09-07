using Given.DataContext.Entities;
using Given.Models;
using Given.Models.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Given.Repositories.Generic
{
    public interface IGiftRepository
    {
        Task<PagedCollection<GiftListModel>> GetAllAsync(QueryParam contactListParam); 
        Task<GiftModel> GetByIdAsync(Guid Giftid);                   
        Task<string> SaveAsync(GiftModel Gift);      
        Task DeleteSaveAsync(GiftModel Gift);        
    }
}                                    