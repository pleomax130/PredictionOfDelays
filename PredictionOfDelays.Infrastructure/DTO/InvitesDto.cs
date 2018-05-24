using System.Collections.Generic;
using PredictionOfDelays.Core.Models;

namespace PredictionOfDelays.Infrastructure.DTO
{
    public class InvitesDto
    {
        public ICollection<EventInviteDto> EventInvites { get; set; }
        public ICollection<GroupInviteDto> GroupInvites { get; set; }
    }
}