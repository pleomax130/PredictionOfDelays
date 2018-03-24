using System.Collections.Generic;
using System.Threading.Tasks;
using PredictionOfDelays.Core.Models;

namespace PredictionOfDelays.Core.Repositories
{
    public interface IUserGroupRepository : IRepository
    {
        RepositoryActionResult<Task> AddAsync(UserGroup userGroup);
        RepositoryActionResult<Task> RemoveAsync(UserGroup userGroup);
        RepositoryActionResult<Task<ICollection<ApplicationUser>>> GetMembersAsync(int groupId);
    }
}