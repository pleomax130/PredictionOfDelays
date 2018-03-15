using System;
using System.Collections.Generic;
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
            try
            {
                await _userEventRepository.AddAsync(new UserEvent { ApplicationUserId = userId, EventId = eventId });
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task RemoveAsync(string userId, int eventId)
        {
            try
            {
                await _userEventRepository.RemoveAsync(new UserEvent {ApplicationUserId = userId, EventId = eventId});
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<ICollection<ApplicationUserDto>> GetAttendeesAsync(int eventId)
        {
            var user = await _userEventRepository.GetAttendeesAsync(eventId);
            return _mapper.Map<ICollection<ApplicationUser>, List<ApplicationUserDto>>(user);
        }
    }
}