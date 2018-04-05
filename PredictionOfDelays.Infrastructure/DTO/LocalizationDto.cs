using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictionOfDelays.Infrastructure.DTO
{
    public class LocalizationDto
    {
        public int LocalizationId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
    }
}
