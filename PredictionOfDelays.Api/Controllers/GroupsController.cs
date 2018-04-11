using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using Microsoft.AspNet.Identity;
using PredictionOfDelays.Core.Models;
using PredictionOfDelays.Infrastructure;
using PredictionOfDelays.Infrastructure.DTO;
using PredictionOfDelays.Infrastructure.Mappers;
using PredictionOfDelays.Infrastructure.Services;
using WebGrease.Css.Extensions;

namespace PredictionOfDelays.Api.Controllers
{
    [RoutePrefix("api/Groups")]
    public class GroupsController : ApiController
    {
        private readonly IGroupService _groupService;
        private readonly IUserGroupService _userGroupService;

        public GroupsController(IGroupService groupService, IUserGroupService userGroupService)
        {
            _groupService = groupService;
            _userGroupService = userGroupService;
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
                var members = await _userGroupService.GetMembersAsync(groupId);
                group.Users = members;
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
            group.OwnerUserId = User.Identity.GetUserId();
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

        [HttpPost]
        [Route("{groupId}/members")]
        public async Task<IHttpActionResult> Join(int groupId)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                await _userGroupService.AddAsync(userId, groupId);
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
        [Route("{groupId}/members")]
        public async Task<IHttpActionResult> Resign(int groupId)
        {
            try
            {
                var userId = User.Identity.GetUserId();
                await _userGroupService.RemoveAsync(userId, groupId);
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
    }
}
