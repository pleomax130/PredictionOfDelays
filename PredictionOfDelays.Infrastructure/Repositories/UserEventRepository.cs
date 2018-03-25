using System;
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

        public async Task<RepositoryActionResult<UserEvent>> AddAsync(UserEvent userEvent)
        {
            var @event = await _context.Events.FirstOrDefaultAsync(e => e.EventId == userEvent.EventId);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userEvent.ApplicationUserId);

            if (@event == null || user == null)
            {
                return new RepositoryActionResult<UserEvent>(userEvent, RepositoryStatus.NotFound);
            }

            try
            {
                _context.UserEvents.Add(userEvent);
                await _context.SaveChangesAsync();
                return new RepositoryActionResult<UserEvent>(userEvent, RepositoryStatus.Created);
            }
            catch (Exception exception)
            {
                return new RepositoryActionResult<UserEvent>(userEvent, RepositoryStatus.Error);
            }
        }

        public async Task<RepositoryActionResult<UserEvent>> RemoveAsync(UserEvent userEvent)
        {
            var ue = await _context.UserEvents.FirstOrDefaultAsync(usEv =>
                usEv.EventId == userEvent.EventId && usEv.ApplicationUserId == userEvent.ApplicationUserId);

            if (ue == null)
            {
                return new RepositoryActionResult<UserEvent>(null, RepositoryStatus.NotFound);
            }

            try
            {
                _context.UserEvents.Remove(userEvent);
                await _context.SaveChangesAsync();
                return new RepositoryActionResult<UserEvent>(userEvent, RepositoryStatus.Deleted);
            }
            catch (Exception exception)
            {
                return new RepositoryActionResult<UserEvent>(userEvent, RepositoryStatus.Error);
            }
        }

        public async Task<RepositoryActionResult<ICollection<ApplicationUser>>> GetAttendeesAsync(int eventId)
        {
            var @event = await _context.Events.FirstOrDefaultAsync(e => e.EventId == eventId);

            if (@event == null)
            {
                return new RepositoryActionResult<ICollection<ApplicationUser>>(null, RepositoryStatus.NotFound);
            }

            var attendees = await _context.UserEvents.Include("AspNetUsers").Where(ue => ue.EventId == eventId)
                .Select(ue => ue.ApplicationUser).ToListAsync();

            return new RepositoryActionResult<ICollection<ApplicationUser>>(attendees, RepositoryStatus.Ok);

        }
    }
}