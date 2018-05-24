using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PredictionOfDelays.Core.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Event> Events { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<UserEvent> UserEvents { get; set; }
        public DbSet<GroupInvite> GroupInvites { get; set; }
        public DbSet<EventInvite> EventInvites { get; set; }
        public DbSet<Localization> Localizations { get; set; }
        public DbSet<ExceptionWrapper> Exception { get; set; }
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}