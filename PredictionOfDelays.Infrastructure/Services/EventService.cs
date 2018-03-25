using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using AutoMapper;
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
            if (result.Status != RepositoryStatus.Ok) throw new Exception();
            var events = await result.Entity.ToListAsync();
            return _mapper.Map<List<Event>, List<EventDto>>(events);
        }

        public async Task<EventDto> GetByIdAsync(int id)
        {
            var result = await _eventRepository.GetByIdAsync(id);
            if (result.Status != RepositoryStatus.Ok) throw new Exception();
            var @event = result.Entity;
            return _mapper.Map<Event, EventDto>(@event);
        }

        public async Task AddAsync(EventDto eventdto)
        {
            var @event = _mapper.Map<EventDto, Event>(eventdto);
            var result = await _eventRepository.AddAsync(@event);
            if (result.Status != RepositoryStatus.Created) throw new Exception();
        }

        public async Task RemoveAsync(int eventId)
        {
            var result = await _eventRepository.RemoveAsync(eventId);
            if (result.Status != RepositoryStatus.Deleted) throw new Exception();
        }

        public async Task UpdateAsync(EventDto eventdto)
        {
            var @event = _mapper.Map<EventDto, Event>(eventdto);
            var result = await _eventRepository.UpdateAsync(@event);
            if (result.Status != RepositoryStatus.Updated) throw new Exception();
        }
    }
}