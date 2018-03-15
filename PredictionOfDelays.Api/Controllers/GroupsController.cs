using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using PredictionOfDelays.Infrastructure.DTO;
using PredictionOfDelays.Infrastructure.Mappers;
using PredictionOfDelays.Infrastructure.Repositories;
using PredictionOfDelays.Infrastructure.Services;

namespace PredictionOfDelays.Api.Controllers
{
    [RoutePrefix("api/Groups")]
    public class GroupsController : ApiController
    {
        private readonly IGroupService _groupService;
        private readonly IUserGroupService _userGroupService;

        public GroupsController()
        {
            _groupService = new GroupService(new GroupRepository(), AutoMapperConfig.Initialize());
        }
        public async Task<IHttpActionResult> Get()
        {
            var groups = await _groupService.GetAsync();
            return Ok(groups);
        }

        [Route("{groupId}")]
        public async Task<IHttpActionResult> Get(int groupId)
        {
            var group = await _groupService.GetByIdAsync(groupId);
            return Ok(group);
        }
        [HttpPost]
        public async Task<IHttpActionResult> CreateGroup([FromBody]GroupDto group)
        {
            await _groupService.AddAsync(group);
            //todo zmien return
            return Ok();
        }

        [HttpDelete]
        [Route("{groupId}")]
        public async Task<IHttpActionResult> DeleteGroup(int groupId)
        {
            await _groupService.RemoveAsync(groupId);
            return Ok();
        }

        [HttpPut]
        public async Task<IHttpActionResult> UpdateGroup([FromBody] GroupDto group)
        {
            await _groupService.UpdateAsync(group);
            return Ok();
        }
    }
}
