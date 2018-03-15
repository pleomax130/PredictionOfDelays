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
            var events = await _eventRepository.GetAllAsync().ToListAsync();
            return _mapper.Map<List<Event>, List<EventDto>>(events);
        }

        public async Task<EventDto> GetByIdAsync(int id)
        {
            var @event = await _eventRepository.GetByIdAsync(id);
            return _mapper.Map<Event, EventDto>(@event);
        }

        public async Task AddAsync(EventDto eventdto)
        {
            try
            {
                var @event = _mapper.Map<EventDto, Event>(eventdto);
                await _eventRepository.AddAsync(@event);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task RemoveAsync(int eventId)
        {
            try
            {
                await _eventRepository.RemoveAsync(eventId);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task UpdateAsync(EventDto eventdto)
        {
            try
            {
                var @event = _mapper.Map<EventDto, Event>(eventdto);
                await _eventRepository.UpdateAsync(@event);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}