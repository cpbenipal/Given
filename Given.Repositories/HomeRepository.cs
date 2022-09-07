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
    public class HomeRepository : IHomeRepository
    {
        private readonly DBGIVENContext dbContext;
        public HomeRepository(DBGIVENContext _dbContext)
        {
            dbContext = _dbContext;
        }
        public async Task<KPIModel> GetKPIs(Guid userid)
        {
            var TotalContacts = await dbContext.Contacts.CountAsync(x => x.UserId == userid);
            var Gifts = await dbContext.Gift.Where(x => x.UserId == userid).ToListAsync();

            var TotalGifts = Gifts.Count();
            var TotalGiftMoney = Gifts.Select(x => x.Amount).Sum();

            var kpis = new KPIModel()
            {
                TotalContacts = TotalContacts,
                TotalGifts = TotalGifts,
                TotalGiftMoney = TotalGiftMoney
            };

            return kpis;
        }
    }
}
