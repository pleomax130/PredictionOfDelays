using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PredictionOfDelays.Core.Models;

namespace PredictionOfDelays.Core.Repositories
{
    public interface IEventRepository : IRepository
    {
        IQueryable<Event> GetAllAsync();
        Task<Event> GetByIdAsync(int id);
        Task UpdateAsync(Event entity);
        Task RemoveAsync(int id);
        Task AddAsync(Event entity);
    }
}
