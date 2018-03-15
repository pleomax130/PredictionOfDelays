namespace PredictionOfDelays.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateAllTables : DbMigration
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
                    "dbo.UserEvents",
                    c => new
                    {
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                        EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new {t.ApplicationUserId, t.EventId})
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.EventId);

            CreateTable(
                    "dbo.UserGroups",
                    c => new
                    {
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new {t.ApplicationUserId, t.GroupId})
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.GroupId);
        }



        public override void Down()
        {
            DropForeignKey("dbo.UserGroups", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.UserGroups", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserEvents", "EventId", "dbo.Events");
            DropForeignKey("dbo.UserEvents", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.GroupApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.GroupApplicationUsers", "Group_GroupId", "dbo.Groups");
            DropForeignKey("dbo.ApplicationUserEvents", "Event_EventId", "dbo.Events");
            DropForeignKey("dbo.ApplicationUserEvents", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.GroupApplicationUsers", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.GroupApplicationUsers", new[] { "Group_GroupId" });
            DropIndex("dbo.ApplicationUserEvents", new[] { "Event_EventId" });
            DropIndex("dbo.ApplicationUserEvents", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.UserGroups", new[] { "GroupId" });
            DropIndex("dbo.UserGroups", new[] { "ApplicationUserId" });
            DropIndex("dbo.UserEvents", new[] { "EventId" });
            DropIndex("dbo.UserEvents", new[] { "ApplicationUserId" });
            DropTable("dbo.GroupApplicationUsers");
            DropTable("dbo.ApplicationUserEvents");
            DropTable("dbo.UserGroups");
            DropTable("dbo.UserEvents");
            DropTable("dbo.Groups");
            DropTable("dbo.Events");
        }
    }
}
