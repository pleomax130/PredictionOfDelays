using PredictionOfDelays.Core.Models;

namespace PredictionOfDelays.Core.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PredictionOfDelays.Core.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PredictionOfDelays.Core.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var g = new Group() {Name = "first"};
            context.Groups.Add(g);
            context.SaveChanges();
            var user = context.Users.FirstOrDefault(u => u.Email == "halo@hallo.com");
            var group = context.Groups.Find(1);
            context.UserGroups.Add(new UserGroup() {Group = group, ApplicationUser = user});
            context.SaveChanges();
        }

        
    }
}
