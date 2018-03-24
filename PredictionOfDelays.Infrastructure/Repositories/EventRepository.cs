﻿using System;
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

        public RepositoryActionResult<IQueryable<Event>> GetAllAsync()
        {
            var events = _context.Events.AsQueryable();
            return events==null ? new RepositoryActionResult<IQueryable<Event>>(_context.Events.AsQueryable(), RepositoryStatus.NotFound) :
                new RepositoryActionResult<IQueryable<Event>>(events, RepositoryStatus.Ok);
        }

        public async Task<RepositoryActionResult<Event>> GetByIdAsync(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return new RepositoryActionResult<Event>(@event, RepositoryStatus.NotFound);
            }
            return new RepositoryActionResult<Event>(@event, RepositoryStatus.Ok);
        }
            

        public async Task<RepositoryActionResult<Event>> UpdateAsync(Event entity)
        {
            if(entity == null)
                return new RepositoryActionResult<Event>(entity, RepositoryStatus.NotFound);
            _context.Entry(entity).State = EntityState.Modified;
             await _context.SaveChangesAsync();
            return new RepositoryActionResult<Event>(entity, RepositoryStatus.Updated);
        }

        public async Task<RepositoryActionResult<Event>> RemoveAsync(int id)
        {
            var e = await _context.Events.FindAsync(id);
            if (e == null)
                return new RepositoryActionResult<Event>(e, RepositoryStatus.NotFound);
            _context.Events.Remove(e);
            await _context.SaveChangesAsync();
            return new RepositoryActionResult<Event>(e,RepositoryStatus.Deleted);
        }

        public async Task<RepositoryActionResult<Event>> AddAsync(Event entity)
        {
            if(entity == null)
                return new RepositoryActionResult<Event>(entity, RepositoryStatus.NotFound);
            _context.Events.Add(entity);
            await _context.SaveChangesAsync();
            return new RepositoryActionResult<Event>(entity, RepositoryStatus.Created);
        }
    }
}
