using System.Collections.Generic;

namespace PredictionOfDelays.Infrastructure.DTO
{
    public class GroupDto
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<ApplicationUserDto> Users { get; set; }
    }
}