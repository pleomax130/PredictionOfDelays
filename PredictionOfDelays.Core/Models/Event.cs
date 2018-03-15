using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictionOfDelays.Core.Models
{
    public class Event : IEntity
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public DateTime EventDate { get; set; }
        public string Description { get; set; }
        public ICollection<ApplicationUser> Users { get; set; }

        //todo add localizatio, administrators and restrictions

        public Event()
        {
           Users = new List<ApplicationUser>();
        }

        
    }
}
