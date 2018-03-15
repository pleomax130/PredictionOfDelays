using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace PredictionOfDelays.Core.Models
{
    public class Group : IEntity
    {
        public int GroupId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<ApplicationUser> Users { get; set; }

        //todo add administrators and restrictions

        public Group()
        {
            Users = new List<ApplicationUser>();
        }
    }
}
