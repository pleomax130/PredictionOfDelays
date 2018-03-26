using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using PredictionOfDelays.Infrastructure;
using PredictionOfDelays.Infrastructure.DTO;
using PredictionOfDelays.Infrastructure.Mappers;
using PredictionOfDelays.Infrastructure.Services;

namespace PredictionOfDelays.Api.Controllers
{
    [RoutePrefix("api/Events")]
    public class EventsController : ApiController
    {
        private readonly IEventService _eventService;
        private readonly IUserEventService _userEventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        public async Task<IHttpActionResult> Get()
        {
            var events = await _eventService.GetAsync();
            return Ok(events);
        }

        [Route("{eventId}")]
        public async Task<IHttpActionResult> Get(int eventId)
        {
            try
            {
                var @event = await _eventService.GetByIdAsync(eventId);
                return Ok(@event);
            }
            catch (ServiceException e)
            {
                if (e.Code == ErrorCodes.EntityNotFound)
                {
                    return NotFound();
                }
                return InternalServerError();
            }
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateEvent([FromBody]EventDto @event)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _eventService.AddAsync(@event);
            return Created(Url.Request.RequestUri+"/"+result.EventId, result);
        }

        [HttpDelete]
        [Route("{eventId}")]
        public async Task<IHttpActionResult> DeleteEvent(int eventId)
        {
            await _eventService.RemoveAsync(eventId);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPut]
        [Route("{eventId}")]
        public async Task<IHttpActionResult> UpdateEvent([FromBody] EventDto @event)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _eventService.UpdateAsync(@event);
            return Ok();
        }
    }
}
