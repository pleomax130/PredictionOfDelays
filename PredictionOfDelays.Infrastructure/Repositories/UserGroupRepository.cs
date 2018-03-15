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

        public async Task AddAsync(UserGroup userGroup)
        {
            _context.UserGroups.Add(userGroup);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(UserGroup userGroup)
        {
            _context.UserGroups.Remove(userGroup);
            await _context.SaveChangesAsync();
        }

        public async Task<ICollection<ApplicationUser>> GetMembersAsync(int groupId)
        {
            var users = await _context.UserGroups.Where(g => g.GroupId == groupId).Select(u=>u.ApplicationUser).ToListAsync();
            return users;
        }
    }
}