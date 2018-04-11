using System.Collections.Generic;
using System.Threading.Tasks;
using PredictionOfDelays.Infrastructure.DTO;

namespace PredictionOfDelays.Infrastructure.Services
{
    public interface IGroupService : IService
    {
        //todo dodac zwracanie obekietu po dodaniu i usunieciu
        Task<ICollection<GroupDto>> GetAsync();
        Task<GroupDto> GetByIdAsync(int id);
        Task<GroupDto> AddAsync(GroupDto group);
        Task RemoveAsync(int groupId);
        Task UpdateAsync(GroupDto group);
    }
}