using System;
using System.Collections.Generic;
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
            try
            {
                await _userGroupRepository.AddAsync(new UserGroup {ApplicationUserId = userId, GroupId = groupId});
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task RemoveAsync(string userId, int groupId)
        {
            try
            {
                await _userGroupRepository.RemoveAsync(new UserGroup {ApplicationUserId = userId, GroupId = groupId});
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<ICollection<ApplicationUserDto>> GetMembersAsync(int groupId)
        {
            var users = await _userGroupRepository.GetMembersAsync(groupId);
            return _mapper.Map<ICollection<ApplicationUser>, List<ApplicationUserDto>>(users);
        }
    }
}