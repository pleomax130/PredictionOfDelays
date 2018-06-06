using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace PredictionOfDelays.Core.Models
{
    public class GroupInvite
    {
        public int GroupInviteId { get; set; }

        public string InvitedId { get; set; }
        public ApplicationUser Invited { get; set; }

        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; }

        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}