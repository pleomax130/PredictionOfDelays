using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using PredictionOfDelays.Core.Models;
using PredictionOfDelays.Core.Repositories;

namespace PredictionOfDelays.Infrastructure.Repositories
{
    public class UserGroupRepository : IUserGroupRepository
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public async Task<RepositoryActionResult<UserGroup>> AddAsync(UserGroup userGroup)
        {
            var group = await _context.Groups.FirstOrDefaultAsync(g => g.GroupId == userGroup.GroupId);
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userGroup.ApplicationUserId);

            if (group == null || user == null)
            {
                return new RepositoryActionResult<UserGroup>(userGroup, RepositoryStatus.NotFound);
            }

            try
            {
                _context.UserGroups.Add(userGroup);
                await _context.SaveChangesAsync();
                return new RepositoryActionResult<UserGroup>(userGroup, RepositoryStatus.Created);
            }
            catch (Exception exception)
            {
                return new RepositoryActionResult<UserGroup>(userGroup, RepositoryStatus.Error);
            }
        }
        
        public async Task<RepositoryActionResult<UserGroup>> RemoveAsync(UserGroup userGroup)
        {
            var ug = await _context.UserGroups.FirstOrDefaultAsync(usGr =>
                usGr.GroupId == userGroup.GroupId && usGr.ApplicationUserId == userGroup.ApplicationUserId);

            if (ug == null)
            {
                return new RepositoryActionResult<UserGroup>(null, RepositoryStatus.NotFound);
            }

            try
            {
                _context.UserGroups.Remove(userGroup);
                await _context.SaveChangesAsync();
                return new RepositoryActionResult<UserGroup>(userGroup, RepositoryStatus.Deleted);
            }
            catch (Exception exception)
            {
                return new RepositoryActionResult<UserGroup>(userGroup, RepositoryStatus.Error);
            }
        }

        public async Task<RepositoryActionResult<IQueryable<ApplicationUser>>> GetMembersAsync(int groupId)
        {
            var group = await _context.Groups.FirstOrDefaultAsync(g => g.GroupId == groupId);

            if (group == null)
            {
                return new RepositoryActionResult<IQueryable<ApplicationUser>>(null, RepositoryStatus.NotFound);
            }

            var attendees = _context.UserGroups.Include("ApplicationUser").Where(ug => ug.GroupId == groupId)
                .Select(ue => ue.ApplicationUser);

            return new RepositoryActionResult<IQueryable<ApplicationUser>>(attendees, RepositoryStatus.Ok);
        }

        public RepositoryActionResult<IQueryable<Group>> GetGroups(string userId)
        {
            var groups = _context.UserGroups.Include("Group").Where(ug => ug.ApplicationUserId == userId)
                .Select(u => u.Group);

            return new RepositoryActionResult<IQueryable<Group>>(groups, RepositoryStatus.Ok);
        }

        public async Task<RepositoryActionResult<GroupInvite>> AddInviteAsync(GroupInvite invite)
        {
            var sender = await _context.Users.FirstOrDefaultAsync(u => u.Id == invite.SenderId);
            var invited = await _context.Users.FirstOrDefaultAsync(u => u.Id == invite.InvitedId);
            var group = await _context.Groups.FirstOrDefaultAsync(e => e.GroupId == invite.GroupId);
            if (sender == null || invited == null || group == null)
            {
                return new RepositoryActionResult<GroupInvite>(invite, RepositoryStatus.NotFound);
            }

            var existingInvite = await _context.GroupInvites.FirstOrDefaultAsync(
                i => i.GroupId == invite.GroupId && i.InvitedId == invite.InvitedId);

            if (existingInvite != null)
            {
                return new RepositoryActionResult<GroupInvite>(existingInvite, RepositoryStatus.BadRequest);
            }

            try
            {
                invite.GroupInviteId = Guid.NewGuid();
                var result = _context.GroupInvites.Add(invite);
                await _context.SaveChangesAsync();
                return new RepositoryActionResult<GroupInvite>(result, RepositoryStatus.Created);
            }
            catch (Exception e)
            {
                return new RepositoryActionResult<GroupInvite>(invite, RepositoryStatus.Error);
            }
        }

        public async Task<RepositoryActionResult<GroupInvite>> AddInviteEmailAsync(int groupId, string senderId, string email)
        {
            var sender = await _context.Users.FirstOrDefaultAsync(u => u.Id == senderId);
            var invited = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            var group = await _context.Groups.FirstOrDefaultAsync(e => e.GroupId == groupId);
            if (sender == null || invited == null || group == null)
            {
                return new RepositoryActionResult<GroupInvite>(null, RepositoryStatus.NotFound);
            }

            var existingInvite = await _context.GroupInvites.FirstOrDefaultAsync(
                i => i.GroupId == groupId && i.Invited.Email == email);

            if (existingInvite != null)
            {
                return new RepositoryActionResult<GroupInvite>(existingInvite, RepositoryStatus.BadRequest);
            }

            try
            {
                var invite = new GroupInvite {GroupId = groupId, SenderId = senderId, InvitedId = invited.Id};
                var result = _context.GroupInvites.Add(invite);
                await _context.SaveChangesAsync();
                return new RepositoryActionResult<GroupInvite>(result, RepositoryStatus.Created);
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
                return new RepositoryActionResult<GroupInvite>(null, RepositoryStatus.Error);
            }
        }

        public async Task<RepositoryActionResult<UserGroup>> AcceptInvitationAsync(Guid inviteId, string receiverId)
        {
            var groupInvite = await _context.GroupInvites.FirstOrDefaultAsync(
                i => i.GroupInviteId == inviteId && i.InvitedId == receiverId);

            if (groupInvite == null)
            {
                return new RepositoryActionResult<UserGroup>(null, RepositoryStatus.NotFound);
            }
            try
            {
                var entity = _context.UserGroups.Add(new UserGroup()
                {
                    ApplicationUserId = groupInvite.InvitedId,
                    GroupId = groupInvite.GroupId
                });
                _context.GroupInvites.Remove(groupInvite);
                await _context.SaveChangesAsync();
                return new RepositoryActionResult<UserGroup>(entity, RepositoryStatus.Created);
            }
            catch (Exception)
            {
                return new RepositoryActionResult<UserGroup>(null, RepositoryStatus.Error);
            }
        }

        public async Task<RepositoryActionResult<GroupInvite>> RejectInvitationAsync(Guid inviteId, string receiverId)
        {
            var groupInvite = await _context.GroupInvites.FirstOrDefaultAsync(
                i => i.GroupInviteId == inviteId && i.InvitedId == receiverId);

            if (groupInvite == null)
            {
                return new RepositoryActionResult<GroupInvite>(null, RepositoryStatus.NotFound);
            }
            try
            {
                _context.GroupInvites.Remove(groupInvite);

                await _context.SaveChangesAsync();
                return new RepositoryActionResult<GroupInvite>(null, RepositoryStatus.Deleted);
            }
            catch (Exception)
            {
                return new RepositoryActionResult<GroupInvite>(null, RepositoryStatus.Error);
            }
        }
    }
}