using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PredictionOfDelays.Core.Models;

namespace PredictionOfDelays.Core.Repositories
{
    public interface IUserGroupRepository : IRepository
    {
        Task<RepositoryActionResult<UserGroup>> AddAsync(UserGroup userGroup);
        Task<RepositoryActionResult<UserGroup>> RemoveAsync(UserGroup userGroup);
        Task<RepositoryActionResult<IQueryable<ApplicationUser>>> GetMembersAsync(int groupId);
        RepositoryActionResult<IQueryable<Group>> GetGroups(string userId);
        Task<RepositoryActionResult<GroupInvite>> AddInviteAsync(GroupInvite invite);
        Task<RepositoryActionResult<UserGroup>> AcceptInvitationAsync(Guid inviteId, string receiverId);
        Task<RepositoryActionResult<GroupInvite>> RejectInvitationAsync(Guid inviteId, string receiverId);
    }
}