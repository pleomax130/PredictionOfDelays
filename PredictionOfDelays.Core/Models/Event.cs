using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictionOfDelays.Core.Models
{
    public class Event : IEntity
    {
        public int EventId { get; set; }
        public string OwnerId { get; set; }
        public ApplicationUser Owner { get; set; }
        [Required]
        [MaxLength(50),MinLength(5)]
        public string Name { get; set; }
        [Required]
        [Display(Name = "Date of event")]
        [FutureDate(ErrorMessage = "Enter future date")]
        public DateTime EventDate { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        public ICollection<UserEvent> Users { get; set; }
        [Required]
        public Localization Localization { get; set; }
        //[NotMapped]
        //public int AmountOfMembers { get; set; }
        public Event()
        {
           Users = new List<UserEvent>();
        }

        
    }
}
