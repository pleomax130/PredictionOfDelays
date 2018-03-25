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
            if (result.Status != RepositoryStatus.Created) throw new Exception();
        }

        public async Task RemoveAsync(string userId, int eventId)
        {
            var result = await _userEventRepository.RemoveAsync(new UserEvent {ApplicationUserId = userId, EventId = eventId});
            if (result.Status != RepositoryStatus.Deleted) throw new Exception();
        }

        public async Task<ICollection<ApplicationUserDto>> GetAttendeesAsync(int eventId)
        {
            var result = await _userEventRepository.GetAttendeesAsync(eventId);
            if (result.Status != RepositoryStatus.Ok) throw new Exception();
            var users = result.Entity.ToList();
            return _mapper.Map<ICollection<ApplicationUser>, List<ApplicationUserDto>>(users);
        }
    }
}