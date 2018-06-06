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

        public async Task AddInviteAsync(string senderId, string invitedId, int groupId)
        {
            var result = await _userGroupRepository.AddInviteAsync(new GroupInvite()
            {
                SenderId = senderId,
                InvitedId = invitedId,
                GroupId = groupId
            });

            switch (result.Status)
            {
                case RepositoryStatus.Created:
                    return;
                case RepositoryStatus.NotFound:
                    throw new ServiceException(ErrorCodes.EntityNotFound);
                case RepositoryStatus.BadRequest:
                    throw new ServiceException(ErrorCodes.BadRequest);
                default: throw new ServiceException(ErrorCodes.DatabaseError);
            }
        }

        public async Task AddInviteEmailAsync(string senderId, string email, int groupId)
        {
            var result = await _userGroupRepository.AddInviteEmailAsync(groupId, senderId, email);

            switch (result.Status)
            {
                case RepositoryStatus.Created:
                    return;
                case RepositoryStatus.NotFound:
                    throw new ServiceException(ErrorCodes.EntityNotFound);
                case RepositoryStatus.BadRequest:
                    throw new ServiceException(ErrorCodes.BadRequest);
                default: throw new ServiceException(ErrorCodes.DatabaseError);
            }
        }

        public async Task AcceptInvitationAsync(int inviteId, string receiverId)
        {
            var result = await _userGroupRepository.AcceptInvitationAsync(inviteId, receiverId);

            switch (result.Status)
            {
                case RepositoryStatus.Created:
                    return;
                case RepositoryStatus.NotFound:
                    throw new ServiceException(ErrorCodes.EntityNotFound);
                default: throw new ServiceException(ErrorCodes.DatabaseError);
            }
        }

        public async Task RejectInvitationAsync(int inviteId, string receiverId)
        {
            var result = await _userGroupRepository.RejectInvitationAsync(inviteId, receiverId);

            switch (result.Status)
            {
                case RepositoryStatus.Deleted:
                    return;
                case RepositoryStatus.NotFound:
                    throw new ServiceException(ErrorCodes.EntityNotFound);
                default: throw new ServiceException(ErrorCodes.DatabaseError);
            }
        }
    }
}