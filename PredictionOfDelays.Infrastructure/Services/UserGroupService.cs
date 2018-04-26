using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using PredictionOfDelays.Core.Models;
using PredictionOfDelays.Core.Repositories;
using PredictionOfDelays.Infrastructure.DTO;

namespace PredictionOfDelays.Infrastructure.Services
{
    public class UserGroupService : IUserGroupService
    {
        private readonly IUserGroupRepository _userGroupRepository;
        private readonly IMapper _mapper;

        public UserGroupService(IUserGroupRepository userGroupRepository, IMapper mapper)
        {
            _userGroupRepository = userGroupRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(string userId, int groupId)
        {
            var result =
                await _userGroupRepository.AddAsync(new UserGroup {ApplicationUserId = userId, GroupId = groupId});

            if (result.Status == RepositoryStatus.NotFound)
            {
                throw new ServiceException(ErrorCodes.BadRequest);
            }
            if (result.Status == RepositoryStatus.Error)
            {
                throw new ServiceException(ErrorCodes.DatabaseError);
            }
        }

        public async Task RemoveAsync(string userId, int groupId)
        {
            var result =
                await _userGroupRepository.RemoveAsync(new UserGroup {ApplicationUserId = userId, GroupId = groupId});
            if (result.Status == RepositoryStatus.NotFound)
            {
                throw new ServiceException(ErrorCodes.EntityNotFound);
            }
            if (result.Status == RepositoryStatus.Error)
            {
                throw new ServiceException(ErrorCodes.DatabaseError);
            }
        }

        public async Task InviteAsync(string userId, int groupId)
        {
            var result =
                await _userGroupRepository.InviteAsync(new UserGroupInvite
                {
                    ApplicationUserId = userId,
                    GroupId = groupId
                });
            if (result.Status == RepositoryStatus.NotFound)
            {
                throw new ServiceException(ErrorCodes.EntityNotFound);
            }
            if (result.Status == RepositoryStatus.Error)
            {
                throw new ServiceException(ErrorCodes.DatabaseError);
            }
        }

        public async Task<List<ApplicationUserDto>> GetMembersAsync(int groupId)
        {
            var result = await _userGroupRepository.GetMembersAsync(groupId);

            if (result.Status == RepositoryStatus.Ok)
            {
                var members = await result.Entity.ToListAsync();
                return _mapper.Map<ICollection<ApplicationUser>, List<ApplicationUserDto>>(members);
            }
            if (result.Status == RepositoryStatus.NotFound)
            {
                throw new ServiceException(ErrorCodes.EntityNotFound);
            }
            throw new ServiceException(ErrorCodes.DatabaseError);
        }

        public async Task<List<GroupDto>> GetGroupsAsync(string userId)
        {
            var result = _userGroupRepository.GetGroups(userId);
            var groups = await result.Entity.ToListAsync();

            return _mapper.Map<ICollection<Group>, List<GroupDto>>(groups);
        }
    }
}