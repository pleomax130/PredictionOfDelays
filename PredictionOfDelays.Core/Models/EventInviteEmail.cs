using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace PredictionOfDelays.Core.Models
{
    public class EventInviteEmail
    {
        [Key]
        public int EventInviteId { get; set; }

        public string Email { get; set; }

        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; }

        public int EventId { get; set; }
        [JsonIgnore]
        public Event Event { get; set; }
    }
}