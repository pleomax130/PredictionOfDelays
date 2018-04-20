﻿using System;
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

        public async Task<List<ApplicationUserDto>> GetAttendeesAsync(int eventId)
        {
            var result = await _userEventRepository.GetAttendeesAsync(eventId);

            if (result.Status == RepositoryStatus.Ok)
            {
                var attendees = await result.Entity.ToListAsync();
                return _mapper.Map<List<ApplicationUser>, List<ApplicationUserDto>>(attendees);
            }
            if (result.Status == RepositoryStatus.NotFound)
            {
                throw new ServiceException(ErrorCodes.EntityNotFound);
            }
            throw new ServiceException(ErrorCodes.DatabaseError);
        }
    }
}