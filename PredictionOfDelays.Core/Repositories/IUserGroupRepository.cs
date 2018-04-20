﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PredictionOfDelays.Core.Models;

namespace PredictionOfDelays.Core.Repositories
{
    public interface IUserGroupRepository : IRepository
    {
        Task<RepositoryActionResult<UserGroup>> AddAsync(UserGroup userGroup);
        Task<RepositoryActionResult<UserGroup>> RemoveAsync(UserGroup userGroup);
        Task<RepositoryActionResult<IQueryable<ApplicationUser>>> GetMembersAsync(int groupId);
    }
}