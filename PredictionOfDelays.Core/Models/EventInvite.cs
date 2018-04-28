using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PredictionOfDelays.Core.Models
{
    public class EventInvite
    {
        [Key]
        public Guid EventInviteId { get; set; }

        public string InvitedId { get; set; }
        public ApplicationUser Invited{ get; set; }

        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; }

        public int EventId { get; set; }
        [JsonIgnore]
        public Event Event { get; set; }
    }
}
