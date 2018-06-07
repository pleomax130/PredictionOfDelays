using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PredictionOfDelays.Core.Models;

namespace PredictionOfDelays.Infrastructure.DTO
{
    public class UserEventDto
    {
        public string ApplicationUserId { get; set; }
        public ApplicationUserDto ApplicationUser { get; set; }
        public int EventId { get; set; }

        public EventDto Event { get; set; }
        public int MinutesOfDelay { get; set; }
        public bool IsAnnounced { get; set; }
    }
}
