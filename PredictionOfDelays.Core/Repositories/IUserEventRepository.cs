﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PredictionOfDelays.Core.Models;

namespace PredictionOfDelays.Core.Repositories
{
    public interface IUserEventRepository : IRepository
    {
        Task<RepositoryActionResult<UserEvent>> AddAsync(UserEvent userEvent);
        Task<RepositoryActionResult<UserEvent>> RemoveAsync(UserEvent userEvent);
        Task<RepositoryActionResult<IQueryable<ApplicationUser>>> GetAttendeesAsync(int eventId);
        RepositoryActionResult<IQueryable<Event>> GetEvents(string userId);
    }
}