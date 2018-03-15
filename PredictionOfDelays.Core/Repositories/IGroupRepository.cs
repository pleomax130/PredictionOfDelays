using System.Linq;
using System.Threading.Tasks;
using PredictionOfDelays.Core.Models;

namespace PredictionOfDelays.Core.Repositories
{
    public interface IGroupRepository : IRepository
    {
        IQueryable<Group> GetAllAsync();
        Task<Group> GetByIdAsync(int id);
        Task UpdateAsync(Group entity);
        Task RemoveAsync(int id);
        Task AddAsync(Group group);
    }
}