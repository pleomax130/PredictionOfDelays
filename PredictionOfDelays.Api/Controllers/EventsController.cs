using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.SignalR;
using PredictionOfDelays.Api.Hubs;
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
        private IHubContext<NotificationsHub> _notificationsHubContext;

        public EventsController(IEventService eventService, IUserEventService userEventService)
        {
            _notificationsHubContext = GlobalHost.DependencyResolver.Resolve<IHubContext<NotificationsHub>>();
            _eventService = eventService;
            _userEventService = userEventService;
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
                var attendees = await _userEventService.GetAttendeesAsync(eventId);
                @event.Users = attendees;
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

            @event.OwnerUserId = User.Identity.GetUserId();
            var result = await _eventService.AddAsync(@event);
            await _userEventService.AddAsync(User.Identity.GetUserId(), result.EventId);
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

        [HttpPost]
        [Route("{eventId}/attendees")]
        public async Task<IHttpActionResult> Join(int eventId)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                await _userEventService.AddAsync(userId, eventId);
                return Ok();
            }
            catch (ServiceException e)
            {
                if (e.Code == ErrorCodes.BadRequest)
                {
                    return BadRequest();
                }
                return InternalServerError();
            }
        }

        [HttpDelete]
        [Route("{eventId}/attendees")]
        public async Task<IHttpActionResult> Resign(int eventId)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                await _userEventService.RemoveAsync(userId, eventId);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (ServiceException e)
            {
                if (e.Code == ErrorCodes.BadRequest)
                {
                    return BadRequest();
                }
                return InternalServerError();
            }
        }

        [HttpDelete]
        [Route("{eventId}/attendees/{userId}")]
        public async Task<IHttpActionResult> Kick(int eventId, string userId)
        {
            try
            {
                var loggedUserId = User.Identity.GetUserId();
                var @event = await _eventService.GetByIdAsync(eventId);
                if (!loggedUserId.Equals(@event.OwnerUserId)) return StatusCode(HttpStatusCode.Unauthorized);
                await _userEventService.RemoveAsync(userId, eventId);
                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (ServiceException e)
            {
                if (e.Code == ErrorCodes.BadRequest)
                {
                    return BadRequest();
                }
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("{eventId}/invites")]
        public async Task<IHttpActionResult> Invite(int eventId, [FromBody]ApplicationUserDto invitedApplicationUserDto)
        {
            try
            {
                var senderId = User.Identity.GetUserId();
                await _userEventService.AddInviteAsync(senderId, invitedApplicationUserDto.Id, eventId);
                return Ok();
            }
            catch (ServiceException e)
            {
                if (e.Code == ErrorCodes.BadRequest)
                {
                    return BadRequest();
                }
                if(e.Code == ErrorCodes.EntityNotFound)
                {
                    return NotFound();
                }
                return InternalServerError();
            }
        }

        [HttpPost]
        [Route("{eventId}/invites/{inviteId}")]
        public async Task<IHttpActionResult> Accept(int eventId, Guid inviteId)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                await _userEventService.AcceptInvitationAsync(inviteId, userId);
                return Ok();
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

        [HttpDelete]
        [Route("{eventId}/invites/{inviteId}")]
        public async Task<IHttpActionResult> Reject(int eventId, Guid inviteId)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                await _userEventService.RejectInvitationAsync(inviteId, userId);
                return StatusCode(HttpStatusCode.NoContent);
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
    }
}
