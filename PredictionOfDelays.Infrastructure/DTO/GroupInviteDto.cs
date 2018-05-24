using System;
using PredictionOfDelays.Core.Models;

namespace PredictionOfDelays.Infrastructure.DTO
{
    public class GroupInviteDto
    {
        public int GroupInviteId { get; set; }

        public string InvitedId { get; set; }
        public ApplicationUserDto Invited { get; set; }

        public string SenderId { get; set; }
        public ApplicationUserDto Sender { get; set; }

        public int GroupId { get; set; }
        public GroupDto Group { get; set; }
    }
}