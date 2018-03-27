using System;
using System.Collections.Generic;
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

        //TODO
        public async Task AddAsync(string userId, int groupId)
        {
            var result = await _userGroupRepository.AddAsync(new UserGroup {ApplicationUserId = userId, GroupId = groupId});
            if (result.Status != RepositoryStatus.Created) throw new Exception();
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

        public async Task<ICollection<ApplicationUserDto>> GetMembersAsync(int groupId)
        {
            var result = await _userGroupRepository.GetMembersAsync(groupId);

            if (result.Status == RepositoryStatus.Ok)
            {
                return _mapper.Map<ICollection<ApplicationUser>, List<ApplicationUserDto>>(result.Entity);
            }
            if (result.Status == RepositoryStatus.NotFound)
            {
                throw new ServiceException(ErrorCodes.EntityNotFound);
            }
            throw new ServiceException(ErrorCodes.DatabaseError);
        }
    }
}