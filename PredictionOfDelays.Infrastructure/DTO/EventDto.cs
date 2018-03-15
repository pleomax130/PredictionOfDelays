using System;

namespace PredictionOfDelays.Infrastructure.DTO
{
    public class EventDto
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public DateTime EventDate { get; set; }
        public string Description { get; set; }
    }
}