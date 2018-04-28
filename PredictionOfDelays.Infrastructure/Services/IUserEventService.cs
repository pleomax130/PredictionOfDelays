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
        Task<EventInvite> AddInviteAsync(string senderId, string invitedId, int eventId);

    }
}