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

        public RepositoryActionResult<IQueryable<Group>> GetAllAsync()
        { 
            var groups =_context.Groups.AsQueryable();
            return new RepositoryActionResult<IQueryable<Group>>(groups, RepositoryStatus.Ok);
        }

        public async Task<RepositoryActionResult<Group>> GetByIdAsync(int id)
        {
            var group = await _context.Groups.FindAsync(id);
            if (group == null)
                return new RepositoryActionResult<Group>(null, RepositoryStatus.NotFound);

            return new RepositoryActionResult<Group>(group, RepositoryStatus.Ok);
        }

        public async Task<RepositoryActionResult<Group>> UpdateAsync(Group entity)
        {
            if(entity == null)
                return new RepositoryActionResult<Group>(null, RepositoryStatus.NotFound);

            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return new RepositoryActionResult<Group>(entity, RepositoryStatus.Updated);
            }
            catch (Exception exception)
            {
                return new RepositoryActionResult<Group>(entity, RepositoryStatus.Error);
            }
        }

        public async Task<RepositoryActionResult<Group>> RemoveAsync(int id)
        {
            var e = await _context.Groups.FindAsync(id);
            if (e == null)
                return new RepositoryActionResult<Group>(null, RepositoryStatus.NotFound);

            try
            {
                _context.Groups.Remove(e);
                await _context.SaveChangesAsync();
                return new RepositoryActionResult<Group>(e, RepositoryStatus.Deleted);
            }
            catch (Exception exception)
            {
                return new RepositoryActionResult<Group>(e, RepositoryStatus.Error);
            }
        }
        
        public async Task<RepositoryActionResult<Group>> AddAsync(Group group)
        {
            try
            {
                _context.Groups.Add(group);
                await _context.SaveChangesAsync();
                return new RepositoryActionResult<Group>(group, RepositoryStatus.Created);
            }
            catch (Exception exception)
            {
                return new RepositoryActionResult<Group>(group, RepositoryStatus.Error);
            }

        }
    }
}
