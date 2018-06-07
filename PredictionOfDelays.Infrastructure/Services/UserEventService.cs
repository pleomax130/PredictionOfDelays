using System;
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
    public class UserEventService : IUserEventService
    {
        private readonly IUserEventRepository _userEventRepository;
        private readonly IMapper _mapper;

        public UserEventService(IUserEventRepository userEventRepository, IMapper mapper)
        {
            _userEventRepository = userEventRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(string userId, int eventId)
        {
            var result = await _userEventRepository.AddAsync(new UserEvent { ApplicationUserId = userId, EventId = eventId });

            if (result.Status == RepositoryStatus.NotFound)
            {
                throw new ServiceException(ErrorCodes.BadRequest);
            }
            if (result.Status == RepositoryStatus.Error)
            {
                throw new ServiceException(ErrorCodes.DatabaseError);
            }
        }

        public async Task RemoveAsync(string userId, int eventId)
        {
            var result = await _userEventRepository.RemoveAsync(new UserEvent {ApplicationUserId = userId, EventId = eventId});
            if (result.Status == RepositoryStatus.NotFound)
            {
                throw new ServiceException(ErrorCodes.EntityNotFound);
            }
            if (result.Status == RepositoryStatus.Error)
            {
                throw new ServiceException(ErrorCodes.DatabaseError);
            }
        }

        public async Task<List<UserEventDto>> GetAttendeesAsync(int eventId)
        {
            var result = await _userEventRepository.GetAttendeesAsync(eventId);

            if (result.Status == RepositoryStatus.Ok)
            {
                var attendees = await result.Entity.ToListAsync();
                return _mapper.Map<List<UserEvent>, List<UserEventDto>>(attendees);
            }
            if (result.Status == RepositoryStatus.NotFound)
            {
                throw new ServiceException(ErrorCodes.EntityNotFound);
            }
            throw new ServiceException(ErrorCodes.DatabaseError);
        }

        public async Task<List<UserEventDto>> GetEventsAsync(string userId)
        {
            var result = _userEventRepository.GetEvents(userId);
            var events = result.Entity.ToList();

            return _mapper.Map<ICollection<UserEvent>, List<UserEventDto>>(events);
        }

        public async Task AddInviteAsync(string senderId, string invitedId, int eventId)
        {
            var result = await _userEventRepository.AddInviteAsync(new EventInvite()
            {
                SenderId = senderId,
                InvitedId = invitedId,
                EventId = eventId
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

        public async Task AddInviteEmailAsync(string senderId, string email, int eventId)
        {
            var result = await _userEventRepository.AddInviteEmailAsync(eventId, senderId, email);

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
            var result = await _userEventRepository.AcceptInvitationAsync(inviteId, receiverId);

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
            var result = await _userEventRepository.RejectInvitationAsync(inviteId, receiverId);

            switch (result.Status)
            {
                case RepositoryStatus.Deleted:
                    return;
                case RepositoryStatus.NotFound:
                    throw new ServiceException(ErrorCodes.EntityNotFound);
                default: throw new ServiceException(ErrorCodes.DatabaseError);
            }
        }

        public async Task<ICollection<string>> GetConnectionIds(string userId)
        {
            return await _userEventRepository.GetConnectionIds(userId);
        }

        public async Task AddConnectionId(string userId, string connectionId)
        {
            await _userEventRepository.AddConnectionId(userId, connectionId);
        }

        public async Task RemoveConnectionId(string userId, string connectionId)
        {
            await _userEventRepository.RemoveConnectionId(userId, connectionId);
        }

        public async Task<Tuple<bool, UserEventDto>> AddDelayAsync(string userId, int eventId, int minutesOfDelay)
        {
            var result = await _userEventRepository.AddDelayAsync(userId, eventId, minutesOfDelay);

            if (result.Status == RepositoryStatus.Ok)
            {
                var userEventDto = _mapper.Map<UserEvent, UserEventDto>(result.Entity.Item2);
                return new Tuple<bool, UserEventDto>(result.Entity.Item1, userEventDto);
            }
            throw new ServiceException(ErrorCodes.EntityNotFound);
        }

        public async Task<int> GetDelayAsync(string userId, int eventId)
        {
            var result = await _userEventRepository.GetDelayAsync(userId, eventId);

            if (result.Status == RepositoryStatus.Ok)
            {
                return result.Entity;
            }
            throw new ServiceException(ErrorCodes.EntityNotFound);
        }

        public async Task<InvitesDto> GetInvites(string userId)
        {
            var result = await _userEventRepository.GetInvites(userId);
            return _mapper.Map<Invites, InvitesDto>(result.Entity);
        }
    }
}