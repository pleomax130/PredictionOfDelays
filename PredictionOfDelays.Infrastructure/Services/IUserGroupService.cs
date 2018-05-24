using System;
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
        Task<List<GroupDto>> GetGroupsAsync(string userId);
        Task AddInviteAsync(string senderId, string email, int groupId);
        Task AddInviteEmailAsync(string senderId, string invitedId, int groupId);
        Task AcceptInvitationAsync(Guid inviteId, string receiverId);
        Task RejectInvitationAsync(Guid inviteId, string receiverId);
    }
}