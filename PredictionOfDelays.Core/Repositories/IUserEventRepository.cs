using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;
using PredictionOfDelays.Core.Models;

namespace PredictionOfDelays.Core.Repositories
{
    public interface IUserEventRepository : IRepository
    {
        Task<RepositoryActionResult<UserEvent>> AddAsync(UserEvent userEvent);
        Task<RepositoryActionResult<UserEvent>> RemoveAsync(UserEvent userEvent);
        Task<RepositoryActionResult<IQueryable<UserEvent>>> GetAttendeesAsync(int eventId);
        RepositoryActionResult<IQueryable<UserEvent>> GetEvents(string userId);
        Task<RepositoryActionResult<EventInvite>> AddInviteAsync(EventInvite invite);
        Task<RepositoryActionResult<EventInvite>> AddGroupInviteAsync(string senderId, int groupId, int eventId);
        Task<RepositoryActionResult<EventInvite>> AddInviteEmailAsync(int eventId, string senderId, string email);
        Task<RepositoryActionResult<UserEvent>> AcceptInvitationAsync(int inviteId, string receiverId);
        Task<RepositoryActionResult<EventInvite>> RejectInvitationAsync(int inviteId, string receiverId);
        Task<RepositoryActionResult<Invites>> GetInvites(string userId);
        Task AddConnectionId(string userId, string connectionId);
        Task RemoveConnectionId(string userId, string connectionId);
        Task<ICollection<string>> GetConnectionIds(string userId);
        Task<RepositoryActionResult<Tuple<bool, UserEvent>>> AddDelayAsync(string userId, int eventId, int minutesOfDelay);
        Task<RepositoryActionResult<int>> GetDelayAsync(string userId, int eventId);
    }
}