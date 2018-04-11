using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using AutoMapper;
using PredictionOfDelays.Core;
using PredictionOfDelays.Core.Models;
using PredictionOfDelays.Core.Repositories;
using PredictionOfDelays.Infrastructure.DTO;

namespace PredictionOfDelays.Infrastructure.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;

        public EventService(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<EventDto>> GetAsync()
        {
            var result = _eventRepository.GetAllAsync();

            var events = await result.Entity.ToListAsync();
            return _mapper.Map<List<Event>, List<EventDto>>(events);
        }

        public async Task<EventDto> GetByIdAsync(int id)
        {
            var result = await _eventRepository.GetByIdAsync(id);

            if (result.Status == RepositoryStatus.Ok)
            {
                var @event = result.Entity;
                return _mapper.Map<Event, EventDto>(@event);
            }
            if (result.Status == RepositoryStatus.NotFound)
            {
                throw new ServiceException(ErrorCodes.EntityNotFound);
            }
            throw new ServiceException(ErrorCodes.DatabaseError);
        }

        public async Task<EventDto> AddAsync(EventDto eventdto)
        {
            var @event = _mapper.Map<EventDto, Event>(eventdto);
            var result = await _eventRepository.AddAsync(@event);

            if (result.Status == RepositoryStatus.Created)
            {
                var entity = result.Entity;
                return _mapper.Map<Event, EventDto>(entity);
            }
            throw new ServiceException(ErrorCodes.DatabaseError);
        }

        public async Task RemoveAsync(int eventId)
        {
            var result = await _eventRepository.RemoveAsync(eventId);

            if (result.Status == RepositoryStatus.NotFound)
            {
                throw new ServiceException(ErrorCodes.EntityNotFound);
            }
            if (result.Status == RepositoryStatus.Error)
            {
                throw new ServiceException(ErrorCodes.DatabaseError);
            }
        }

        public async Task UpdateAsync(EventDto eventdto)
        {
            var @event = _mapper.Map<EventDto, Event>(eventdto);
            var result = await _eventRepository.UpdateAsync(@event);

            if (result.Status == RepositoryStatus.Error)
            {
                throw new ServiceException(ErrorCodes.DatabaseError);
            }
        }
    }
}