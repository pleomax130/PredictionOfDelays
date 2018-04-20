using System.Collections.Generic;
using System.Threading.Tasks;
using PredictionOfDelays.Core.Models;
using PredictionOfDelays.Infrastructure.DTO;

namespace PredictionOfDelays.Infrastructure.Services
{
    public interface IUserGroupService : IService
    {
        Task AddAsync(string userId, int groupId);
        Task RemoveAsync(string userId, int groupId);
        Task<List<ApplicationUserDto>> GetMembersAsync(int groupId);
        Task<int> GetAmountOfMembers(int groupId);
    }
}