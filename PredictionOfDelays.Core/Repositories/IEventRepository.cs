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
        RepositoryActionResult<IQueryable<Event>> GetAllAsync();
        Task<RepositoryActionResult<Event>> GetByIdAsync(int id);
        Task<RepositoryActionResult<Event>> UpdateAsync(Event entity);
        Task<RepositoryActionResult<Event>> RemoveAsync(int id);
        Task<RepositoryActionResult<Event>> AddAsync(Event entity);
    }
}
