using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using PredictionOfDelays.Core.Models;
using PredictionOfDelays.Core.Repositories;
using PredictionOfDelays.Infrastructure.Utilities;

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

        public async Task<RepositoryActionResult<IQueryable<UserEvent>>> GetAttendeesAsync(int eventId)
        {
            var @event = await _context.Events.FirstOrDefaultAsync(e => e.EventId == eventId);

            if (@event == null)
            {
                return new RepositoryActionResult<IQueryable<UserEvent>>(null, RepositoryStatus.NotFound);
            }

            var attendees = _context.UserEvents.Include("ApplicationUser").Where(ue => ue.EventId == eventId);

            return new RepositoryActionResult<IQueryable<UserEvent>>(attendees, RepositoryStatus.Ok);

        }

        public RepositoryActionResult<IQueryable<UserEvent>> GetEvents(string userId)
        {
            var events = _context.UserEvents.Include("Event.Localization").Where(ug => ug.ApplicationUserId == userId).ToList().AsQueryable();
//            var x = _context.UserEvents.Include("Event.Localization").Where(ug => ug.ApplicationUserId == userId).ToList();
            return new RepositoryActionResult<IQueryable<UserEvent>>(events, RepositoryStatus.Ok);
        }

        public async Task<RepositoryActionResult<EventInvite>> AddInviteAsync(EventInvite invite)
        {
            var sender = await _context.Users.FirstOrDefaultAsync(u => u.Id == invite.SenderId);
            var invited = await _context.Users.FirstOrDefaultAsync(u => u.Id == invite.InvitedId);
            var @event = await _context.Events.FirstOrDefaultAsync(e => e.EventId == invite.EventId);
            if (sender == null || invited == null || @event == null)
            {
                return new RepositoryActionResult<EventInvite>(invite, RepositoryStatus.NotFound);
            }

            var existingInvite = await _context.EventInvites.FirstOrDefaultAsync(
                i => i.EventId == invite.EventId && i.InvitedId == invite.InvitedId);

            if (existingInvite != null)
            {
                return new RepositoryActionResult<EventInvite>(existingInvite, RepositoryStatus.BadRequest);
            }

            try
            {
                var result = _context.EventInvites.Add(invite);
                await _context.SaveChangesAsync();
//                new InviteSender().SendEventInvite(invite);
                return new RepositoryActionResult<EventInvite>(result, RepositoryStatus.Created);
            }
            catch (Exception e)
            {
                return new RepositoryActionResult<EventInvite>(invite, RepositoryStatus.Error);
            }
        }

        public async Task<RepositoryActionResult<EventInvite>> AddGroupInviteAsync(string senderId, int groupId, int eventId)
        {
            var group = await _context.Groups.FirstOrDefaultAsync(g => g.GroupId == groupId);
            if (@group == null) return new RepositoryActionResult<EventInvite>(null, RepositoryStatus.Error);
            var users = @group.Users;
            foreach (var user in users)
            {
                var existingInvite = await _context.EventInvites.FirstOrDefaultAsync(
                    i => i.EventId == eventId && i.InvitedId == user.ApplicationUserId);

                if (existingInvite != null)
                {
                    return new RepositoryActionResult<EventInvite>(existingInvite, RepositoryStatus.BadRequest);
                }

                try
                {
                    var invite = new EventInvite {SenderId = senderId, InvitedId = user.ApplicationUserId, EventId = eventId};
                    var result = _context.EventInvites.Add(invite);
                    await _context.SaveChangesAsync();
                    new InviteSender().SendEventInvite(invite);
                    return new RepositoryActionResult<EventInvite>(result, RepositoryStatus.Created);
                }
                catch (Exception e)
                {
                    return new RepositoryActionResult<EventInvite>(null, RepositoryStatus.Error);
                }
            }
            return new RepositoryActionResult<EventInvite>(null, RepositoryStatus.Error);
        }

        public async Task<RepositoryActionResult<EventInvite>> AddInviteEmailAsync(int eventId, string senderId, string email)
        {
            var sender = await _context.Users.FirstOrDefaultAsync(u => u.Id == senderId);
            var invited = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            var @event = await _context.Events.FirstOrDefaultAsync(e => e.EventId == eventId);
            if (sender == null || invited == null || @event == null)
            {
                return new RepositoryActionResult<EventInvite>(null, RepositoryStatus.NotFound);
            }

            var existingInvite = await _context.EventInvites.FirstOrDefaultAsync(
                i => i.EventId == eventId && i.Invited.Email == email);

            if (existingInvite != null)
            {
                return new RepositoryActionResult<EventInvite>(existingInvite, RepositoryStatus.BadRequest);
            }

            try
            {
                var invite = new EventInvite {EventId = eventId, SenderId = senderId, InvitedId = invited.Id};
                var result = _context.EventInvites.Add(invite);
                await _context.SaveChangesAsync();
                //new InviteSender().SendEventInvite(invite);
                return new RepositoryActionResult<EventInvite>(result, RepositoryStatus.Created);
            }
            catch (Exception e)
            {
                var ex = new ExceptionWrapper
                {
                    ExceptionMessage = e.Message,
                    ExceptionStackTrace = e.StackTrace,
                    LogTime = DateTime.Now
                };
                _context.Exception.Add(ex);
                await _context.SaveChangesAsync();
                return new RepositoryActionResult<EventInvite>(null, RepositoryStatus.Error);
            }
        }

     

        public async Task<RepositoryActionResult<UserEvent>> AcceptInvitationAsync(int inviteId, string receiverId)
        {
            var eventInvite = await _context.EventInvites.Include(e => e.Event).FirstOrDefaultAsync(
                i => i.EventInviteId == inviteId && i.InvitedId == receiverId);

            if (eventInvite == null)
            {
                return new RepositoryActionResult<UserEvent>(null, RepositoryStatus.NotFound);
            }
            try
            {
                var entity = _context.UserEvents.Add(new UserEvent()
                {
                    ApplicationUserId = eventInvite.InvitedId,
                    EventId = eventInvite.EventId
                });
                await _context.SaveChangesAsync();
                //Clean invites
                _context.EventInvites.Remove(eventInvite);

                await _context.SaveChangesAsync();
                return new RepositoryActionResult<UserEvent>(entity, RepositoryStatus.Created);
            }
            catch (Exception)
            {
                return new RepositoryActionResult<UserEvent>(null, RepositoryStatus.Error);
            }
        }

        public async Task<RepositoryActionResult<EventInvite>> RejectInvitationAsync(int inviteId, string receiverId)
        {
            var eventInvite = await _context.EventInvites.FirstOrDefaultAsync(
                i => i.EventInviteId == inviteId && i.InvitedId == receiverId);

            if (eventInvite == null)
            {
                return new RepositoryActionResult<EventInvite>(null, RepositoryStatus.NotFound);
            }
            try
            {
                _context.EventInvites.Remove(eventInvite);

                await _context.SaveChangesAsync();
                return new RepositoryActionResult<EventInvite>(null, RepositoryStatus.Deleted);
            }
            catch (Exception)
            {
                return new RepositoryActionResult<EventInvite>(null, RepositoryStatus.Error);
            }
        }

        public async Task AddConnectionId(string userId, string connectionId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            user?.ConnectionIds.Add(connectionId);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveConnectionId(string userId, string connectionId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            user?.ConnectionIds.Remove(connectionId);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<string>> GetConnectionIds(string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            return user?.ConnectionIds;
        }

        public async Task AddDelayAsync(string userId, int eventId, int minutesOfDelay)
        {
            var userEvent =
                await _context.UserEvents.FirstOrDefaultAsync(ue => ue.ApplicationUserId == userId && ue.EventId == eventId);

            if (userEvent != null)
            {
                userEvent.MinutesOfDelay = minutesOfDelay;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<RepositoryActionResult<int>> GetDelayAsync(string userId, int eventId)
        {
            var userEvent =
                await _context.UserEvents.FirstOrDefaultAsync(ue => ue.ApplicationUserId == userId && ue.EventId == eventId);

            if (userEvent != null)
            {
                return new RepositoryActionResult<int>(userEvent.MinutesOfDelay, RepositoryStatus.Ok);
            }
            return new RepositoryActionResult<int>(0, RepositoryStatus.NotFound);
        }

        public async Task<RepositoryActionResult<Invites>> GetInvites(string userId)
        {
            var eventInvites = await _context.EventInvites.Include(e => e.Event).Include(e => e.Sender).Include(e => e.Invited).Where(ev => ev.InvitedId == userId).ToListAsync();
            var groupInvites = await _context.GroupInvites.Include(g => g.Group).Include(g => g.Sender).Include(g => g.Invited).Where(ev => ev.InvitedId == userId).ToListAsync();

            var invites = new Invites() {EventInvites = eventInvites, GroupInvites = groupInvites};
            return new RepositoryActionResult<Invites>(invites, RepositoryStatus.Ok);
        }
    }
}