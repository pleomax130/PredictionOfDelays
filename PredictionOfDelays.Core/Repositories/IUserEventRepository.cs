using System.Collections.Generic;
using System.Threading.Tasks;
using PredictionOfDelays.Core.Models;

namespace PredictionOfDelays.Core.Repositories
{
    public interface IUserEventRepository : IRepository
    {
        RepositoryActionResult<Task> AddAsync(UserEvent userEvent);
        RepositoryActionResult<Task> RemoveAsync(UserEvent userEvent);
        RepositoryActionResult<Task<ICollection<ApplicationUser>>> GetAttendeesAsync(int eventId);
    }
}