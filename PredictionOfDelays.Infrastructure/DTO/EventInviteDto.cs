using System;
using PredictionOfDelays.Core.Models;

namespace PredictionOfDelays.Infrastructure.DTO
{
    public class EventInviteDto
    {
        public int EventInviteId { get; set; }

        public string InvitedId { get; set; }
        public ApplicationUserDto Invited{ get; set; }

        public string SenderId { get; set; }
        public ApplicationUserDto Sender { get; set; }

        public int EventId { get; set; }
        public EventDto Event { get; set; }
    }
}