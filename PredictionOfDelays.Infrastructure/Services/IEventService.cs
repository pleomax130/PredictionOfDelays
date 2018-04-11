using System.Collections.Generic;
using System.Threading.Tasks;
using PredictionOfDelays.Infrastructure.DTO;

namespace PredictionOfDelays.Infrastructure.Services
{
    public interface IEventService : IService
    {
        Task<ICollection<EventDto>> GetAsync();
        Task<EventDto> GetByIdAsync(int id);
        Task<EventDto> AddAsync(EventDto @event);
        Task RemoveAsync(int eventId);
        Task UpdateAsync(EventDto @event);
    }
}