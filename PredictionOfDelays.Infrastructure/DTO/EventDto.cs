using System;
using System.Collections.Generic;

namespace PredictionOfDelays.Infrastructure.DTO
{
    public class EventDto
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public DateTime EventDate { get; set; }
        public string Description { get; set; }
        public List<ApplicationUserDto> Users { get; set; }
    }
}