using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using PredictionOfDelays.Infrastructure;
using PredictionOfDelays.Infrastructure.DTO;
using PredictionOfDelays.Infrastructure.Mappers;
using PredictionOfDelays.Infrastructure.Services;

namespace PredictionOfDelays.Api.Controllers
{
    [RoutePrefix("api/Groups")]
    public class GroupsController : ApiController
    {
        private readonly IGroupService _groupService;
        private readonly IUserGroupService _userGroupService;

        public GroupsController(IGroupService groupService)
        {
            _groupService = groupService;
        }
        public async Task<IHttpActionResult> Get()
        {
            var groups = await _groupService.GetAsync();
            return Ok(groups);
        }

        [Route("{groupId}")]
        public async Task<IHttpActionResult> Get(int groupId)
        {
            try
            {
                var group = await _groupService.GetByIdAsync(groupId);
                return Ok(group);
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
        public async Task<IHttpActionResult> CreateGroup([FromBody]GroupDto group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _groupService.AddAsync(group);
            return Created(Url.Request.RequestUri + "/" + result.GroupId, result);
        }

        [HttpDelete]
        [Route("{groupId}")]
        public async Task<IHttpActionResult> DeleteGroup(int groupId)
        {
            await _groupService.RemoveAsync(groupId);
            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPut]
        public async Task<IHttpActionResult> UpdateGroup([FromBody] GroupDto group)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _groupService.UpdateAsync(group);
            return Ok();
        }
    }
}
