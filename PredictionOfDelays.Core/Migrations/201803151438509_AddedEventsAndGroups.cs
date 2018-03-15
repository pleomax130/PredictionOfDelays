namespace PredictionOfDelays.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEventsAndGroups : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        EventDate = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.EventId);
            
            
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        GroupId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.GroupId);

            
            CreateTable(
                "dbo.ApplicationUserEvents",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Event_EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Event_EventId })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Events", t => t.Event_EventId, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Event_EventId);
            
            CreateTable(
                "dbo.GroupApplicationUsers",
                c => new
                    {
                        Group_GroupId = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Group_GroupId, t.ApplicationUser_Id })
                .ForeignKey("dbo.Groups", t => t.Group_GroupId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .Index(t => t.Group_GroupId)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.GroupApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.GroupApplicationUsers", "Group_GroupId", "dbo.Groups");
            DropForeignKey("dbo.ApplicationUserEvents", "Event_EventId", "dbo.Events");
            DropForeignKey("dbo.ApplicationUserEvents", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.GroupApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.GroupApplicationUsers", new[] { "Group_GroupId" });
            DropIndex("dbo.ApplicationUserEvents", new[] { "Event_EventId" });
            DropIndex("dbo.ApplicationUserEvents", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropTable("dbo.GroupApplicationUsers");
            DropTable("dbo.ApplicationUserEvents");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.Groups");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Events");
        }
    }
}
