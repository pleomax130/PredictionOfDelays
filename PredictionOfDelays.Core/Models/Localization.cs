using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictionOfDelays.Core.Models
{
    public class Localization
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public Localization()
        {
            
        }

        public Localization(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
