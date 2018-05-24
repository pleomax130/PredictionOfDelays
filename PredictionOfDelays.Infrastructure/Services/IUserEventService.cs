using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PredictionOfDelays.Core.Models;
using PredictionOfDelays.Infrastructure.DTO;

namespace PredictionOfDelays.Infrastructure.Services
{
    public interface IUserEventService : IService
    {
        Task AddAsync(string userId, int eventId);
        Task RemoveAsync(string userId, int eventId);
        Task<List<ApplicationUserDto>> GetAttendeesAsync(int eventId);
        Task<List<EventDto>> GetEventsAsync(string userId);
        Task AddInviteAsync(string senderId, string invitedId, int eventId);
        Task AddInviteEmailAsync(string senderId, string email, int eventId);
        Task AcceptInvitationAsync(Guid inviteId, string receiverId);
        Task RejectInvitationAsync(Guid inviteId, string receiverId);
        Task<InvitesDto> GetInvites(string userId);
        Task<ICollection<string>> GetConnectionIds(string userId);
        Task AddConnectionId(string userId, string connectionId);
        Task RemoveConnectionId(string userId, string connectionId);
    }
}