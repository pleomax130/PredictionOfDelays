using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PredictionOfDelays.Core.Models;
using PredictionOfDelays.Core.Repositories;

namespace PredictionOfDelays.Infrastructure.Repositories
{
    public class UserEventRepository : IUserEventRepository
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public async Task AddAsync(UserEvent userEvent)
        {
            _context.UserEvents.Add(userEvent);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(UserEvent userEvent)
        {
            _context.UserEvents.Remove(userEvent);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<ApplicationUser>> GetAttendeesAsync(int eventId)
        {
            var users = await _context.UserEvents.Where(ue => ue.EventId == eventId).Select(u => u.ApplicationUser).ToListAsync();
            return users;
        }
    }
}