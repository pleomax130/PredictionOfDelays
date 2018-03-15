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
    public class GroupService : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;

        public GroupService(IGroupRepository groupRepository, IMapper mapper)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
        }

        public async Task<ICollection<GroupDto>> GetAsync()
        {
            var groups = await _groupRepository.GetAllAsync().ToListAsync();
            return _mapper.Map<List<Group>, List<GroupDto>>(groups);
        }

        public async Task<GroupDto> GetByIdAsync(int id)
        {
            var group = await _groupRepository.GetByIdAsync(id);
            return _mapper.Map<Group, GroupDto>(group);
        }

        public async Task AddAsync(GroupDto groupDto)
        {
            try
            {
                var group = _mapper.Map<GroupDto, Group>(groupDto);
                await _groupRepository.AddAsync(group);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task RemoveAsync(int groupId)
        {
            try
            {
                await _groupRepository.RemoveAsync(groupId);
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task UpdateAsync(GroupDto groupDto)
        {
            try
            {
                var group = _mapper.Map<GroupDto, Group>(groupDto);
                await _groupRepository.UpdateAsync(group);
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}