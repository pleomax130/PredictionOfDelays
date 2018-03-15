namespace PredictionOfDelays.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Revert : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ApplicationUserEvents", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationUserEvents", "Event_EventId", "dbo.Events");
            DropForeignKey("dbo.GroupApplicationUsers", "Group_GroupId", "dbo.Groups");
            DropForeignKey("dbo.GroupApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserEvents", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUserEvents", new[] { "Event_EventId" });
            DropIndex("dbo.GroupApplicationUsers", new[] { "Group_GroupId" });
            DropIndex("dbo.GroupApplicationUsers", new[] { "ApplicationUser_Id" });
            DropTable("dbo.Events");
            DropTable("dbo.Groups");
            DropTable("dbo.ApplicationUserEvents");
            DropTable("dbo.GroupApplicationUsers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.GroupApplicationUsers",
                c => new
                    {
                        Group_GroupId = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Group_GroupId, t.ApplicationUser_Id });
            
            CreateTable(
                "dbo.ApplicationUserEvents",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Event_EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.Event_EventId });
            
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
                "dbo.Events",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        EventDate = c.DateTime(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.EventId);
            
            CreateIndex("dbo.GroupApplicationUsers", "ApplicationUser_Id");
            CreateIndex("dbo.GroupApplicationUsers", "Group_GroupId");
            CreateIndex("dbo.ApplicationUserEvents", "Event_EventId");
            CreateIndex("dbo.ApplicationUserEvents", "ApplicationUser_Id");
            AddForeignKey("dbo.GroupApplicationUsers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.GroupApplicationUsers", "Group_GroupId", "dbo.Groups", "GroupId", cascadeDelete: true);
            AddForeignKey("dbo.ApplicationUserEvents", "Event_EventId", "dbo.Events", "EventId", cascadeDelete: true);
            AddForeignKey("dbo.ApplicationUserEvents", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
