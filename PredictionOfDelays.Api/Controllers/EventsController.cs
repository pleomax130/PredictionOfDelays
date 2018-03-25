using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using PredictionOfDelays.Infrastructure.DTO;
using PredictionOfDelays.Infrastructure.Mappers;
using PredictionOfDelays.Infrastructure.Repositories;
using PredictionOfDelays.Infrastructure.Services;

namespace PredictionOfDelays.Api.Controllers
{
    [RoutePrefix("api/Events")]
    public class EventsController : ApiController
    {
        private readonly IEventService _eventService;
        private readonly IUserEventService _userEventService;

        public EventsController()
        {
            _eventService = new EventService(new EventRepository(), AutoMapperConfig.Initialize());
        }

        public async Task<IHttpActionResult> Get()
        {
            var events = await _eventService.GetAsync();
            return Ok(events);
        }

        [Route("{eventId}")]
        public async Task<IHttpActionResult> Get(int eventId)
        {
            var @event = await _eventService.GetByIdAsync(eventId);
            return Ok(@event);
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateEvent([FromBody]EventDto @event)
        {
            await _eventService.AddAsync(@event);
            return Ok();
        }

        [HttpDelete]
        [Route("{eventId}")]
        public async Task<IHttpActionResult> DeleteEvent(int eventId)
        {
            await _eventService.RemoveAsync(eventId);
            return Ok();
        }

        [HttpPut]
        public async Task<IHttpActionResult> UpdateEvent([FromBody] EventDto @event)
        {
            await _eventService.UpdateAsync(@event);
            return Ok();
        }
    }
}
