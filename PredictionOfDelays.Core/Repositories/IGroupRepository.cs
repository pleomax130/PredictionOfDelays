using System.Linq;
using System.Threading.Tasks;
using PredictionOfDelays.Core.Models;

namespace PredictionOfDelays.Core.Repositories
{
    public interface IGroupRepository : IRepository
    {
        RepositoryActionResult<IQueryable<Group>> GetAllAsync();
        Task<RepositoryActionResult<Group>> GetByIdAsync(int id);
        Task<RepositoryActionResult<Group>> UpdateAsync(Group entity);
        Task<RepositoryActionResult<Group>> RemoveAsync(int id);
        Task<RepositoryActionResult<Group>> AddAsync(Group group);
    }
}