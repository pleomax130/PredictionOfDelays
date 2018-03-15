using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using PredictionOfDelays.Core.Models;
using PredictionOfDelays.Core.Repositories;

namespace PredictionOfDelays.Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly ApplicationDbContext _context = new ApplicationDbContext();

        public IQueryable<Event> GetAllAsync()
            => _context.Events.AsQueryable();

        public async Task<Event> GetByIdAsync(int id)
            => await _context.Events.FindAsync(id);

        public async Task UpdateAsync(Event entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
             await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(int id)
        {
            var e = await _context.Events.FindAsync(id);

            if (e != null)
            {
                _context.Events.Remove(e);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddAsync(Event entity)
        {
            _context.Events.Add(entity);
            await _context.SaveChangesAsync();
        }
    }
}
