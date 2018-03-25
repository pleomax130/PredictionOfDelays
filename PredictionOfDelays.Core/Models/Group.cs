using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace PredictionOfDelays.Core.Models
{
    public class Group : IEntity
    {
        public int GroupId { get; set; }

        [Required]
        [MaxLength(40), MinLength(5)]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Description { get; set; }
        public ICollection<ApplicationUser> Users { get; set; }

        //todo add administrators and restrictions

        public Group()
        {
            Users = new List<ApplicationUser>();
        }
    }
}
