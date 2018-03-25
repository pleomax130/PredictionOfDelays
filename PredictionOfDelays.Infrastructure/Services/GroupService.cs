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
            var result = _groupRepository.GetAllAsync();
            if (result.Status != RepositoryStatus.Ok) throw new Exception();
            var groups = await result.Entity.ToListAsync();
            return _mapper.Map<List<Group>, List<GroupDto>>(groups);
        }

        public async Task<GroupDto> GetByIdAsync(int id)
        {
            var result = await _groupRepository.GetByIdAsync(id);
            if (result.Status != RepositoryStatus.Ok) throw new Exception();
            var group = result.Entity;
            return _mapper.Map<Group, GroupDto>(group);
        }

        public async Task AddAsync(GroupDto groupDto)
        {
            var group = _mapper.Map<GroupDto, Group>(groupDto);
            var result = await _groupRepository.AddAsync(group);
            if (result.Status != RepositoryStatus.Created) throw new Exception();
        }

        public async Task RemoveAsync(int groupId)
        {
            var result = await _groupRepository.RemoveAsync(groupId);
            if (result.Status != RepositoryStatus.Deleted) throw new Exception();
        }

        public async Task UpdateAsync(GroupDto groupDto)
        {
            var group = _mapper.Map<GroupDto, Group>(groupDto);
            var result = await _groupRepository.UpdateAsync(group);
            if (result.Status != RepositoryStatus.Updated) throw new Exception();
        }
    }
}