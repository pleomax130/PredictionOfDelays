using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PredictionOfDelays.Core.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Event> Events { get; set; }
        public ICollection<Group> Groups { get; set; }
        [NotMapped]
        public ICollection<string> ConnectionIds { get; set; }
        public string ConnectionIdsAsString
        {
            get { return string.Join(",", ConnectionIds); }
            set
            {
                if (value != null)
                {
                    ConnectionIds = value.Split(',').ToList();
                }
                else
                {
                    ConnectionIds = new List<string>();
                }
            }
        }

        public ApplicationUser()
        {
            Events = new List<Event>();
            Groups = new List<Group>();
            ConnectionIds = new List<string>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}