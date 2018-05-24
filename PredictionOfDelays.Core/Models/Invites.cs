using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PredictionOfDelays.Core.Models
{
    public class Invites
    {
        public ICollection<EventInvite> EventInvites { get; set; }
        public ICollection<GroupInvite> GroupInvites { get; set; }
    }
}
