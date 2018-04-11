using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictionOfDelays.Core.Models
{
    public class Localization
    {
        public int LocalizationId { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public Localization()
        {
            
        }

        public Localization(string latitude, string longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
