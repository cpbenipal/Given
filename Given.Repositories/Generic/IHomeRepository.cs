using Given.DataContext.Entities;
using Given.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Given.Repositories.Generic
{
    public interface IHomeRepository
    {
        Task<KPIModel> GetKPIs(Guid userid);  
    }
}