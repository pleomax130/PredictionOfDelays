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
            var userIds =  _context.Users.Select(u => u.Id).ToList();

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

        public async Task<RepositoryActionResult<int>> GetAmountOfMembersAsync(int groupId)
        {
            var group = await _context.Groups.FirstOrDefaultAsync(e => e.GroupId == groupId);

            if (group == null)
            {
                return new RepositoryActionResult<int>(0, RepositoryStatus.NotFound);
            }

            var amountOfMembers = _context.UserGroups.Count(ug => ug.GroupId == groupId);

            return new RepositoryActionResult<int>(amountOfMembers, RepositoryStatus.Ok);
        }
    }
}