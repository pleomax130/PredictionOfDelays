using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PredictionOfDelays.Core.Models
{
    public class Event : IEntity
    {
        public int EventId { get; set; }
        public string OwnerUserId { get; set; }
        [Required]
        [MaxLength(50),MinLength(5)]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Date of event")]
        public DateTime EventDate { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        public ICollection<UserEvent> Users { get; set; }
        [Required]
        public Localization Localization { get; set; }
        [JsonIgnore]
        public ICollection<EventInvite> EventInvites { get; set; }

        public Event()
        {
           Users = new List<UserEvent>();
           EventInvites = new List<EventInvite>();
        }

        
    }
}
