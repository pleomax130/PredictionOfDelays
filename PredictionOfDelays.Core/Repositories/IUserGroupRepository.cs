using System.Collections.Generic;
using System.Threading.Tasks;
using PredictionOfDelays.Core.Models;

namespace PredictionOfDelays.Core.Repositories
{
    public interface IUserGroupRepository : IRepository
    {
        Task AddAsync(UserGroup userGroup);
        Task RemoveAsync(UserGroup userGroup);
        Task<ICollection<ApplicationUser>> GetMembersAsync(int groupId);
    }
}