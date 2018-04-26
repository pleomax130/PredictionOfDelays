using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PredictionOfDelays.Core.Models
{
    public class UserGroupInvite
    {
        [Key, Column(Order = 0)]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        [Key, Column(Order = 1)]
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}