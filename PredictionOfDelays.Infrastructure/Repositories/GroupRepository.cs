using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PredictionOfDelays.Core.Models;
using PredictionOfDelays.Core.Repositories;

namespace PredictionOfDelays.Infrastructure.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public IQueryable<Group> GetAllAsync()
            => _context.Groups.AsQueryable();

        public async Task<Group> GetByIdAsync(int id)
            => await _context.Groups.FindAsync(id);

        public async Task UpdateAsync(Group entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var e = await _context.Groups.FindAsync(id);
            if (e != null)
            {
                _context.Groups.Remove(e);
                await _context.SaveChangesAsync();
            }
        }
        
        public async Task AddAsync(Group group)
        {
            _context.Groups.Add(group);
            await _context.SaveChangesAsync();
        }
    }
}
