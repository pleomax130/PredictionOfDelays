using System.Collections.Generic;
using System.Threading.Tasks;
using PredictionOfDelays.Core.Models;

namespace PredictionOfDelays.Core.Repositories
{
    public interface IUserEventRepository : IRepository
    {
        Task AddAsync(UserEvent userEvent);
        Task RemoveAsync(UserEvent userEvent);
        Task<ICollection<ApplicationUser>> GetAttendeesAsync(int eventId);
    }
}