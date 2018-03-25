using System.Collections.Generic;
using System.Threading.Tasks;
using PredictionOfDelays.Core.Models;

namespace PredictionOfDelays.Core.Repositories
{
    public interface IUserGroupRepository : IRepository
    {
        Task<RepositoryActionResult<UserGroup>> AddAsync(UserGroup userGroup);
        Task<RepositoryActionResult<UserGroup>> RemoveAsync(UserGroup userGroup);
        Task<RepositoryActionResult<ICollection<ApplicationUser>>> GetMembersAsync(int groupId);
    }
}