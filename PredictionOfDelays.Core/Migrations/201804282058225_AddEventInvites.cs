namespace PredictionOfDelays.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEventInvites : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EventInvites",
                c => new
                    {
                        EventInviteId = c.Guid(nullable: false),
                        InvitedId = c.String(maxLength: 128),
                        SenderId = c.String(maxLength: 128),
                        EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EventInviteId)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.InvitedId)
                .ForeignKey("dbo.AspNetUsers", t => t.SenderId)
                .Index(t => t.InvitedId)
                .Index(t => t.SenderId)
                .Index(t => t.EventId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventInvites", "SenderId", "dbo.AspNetUsers");
            DropForeignKey("dbo.EventInvites", "InvitedId", "dbo.AspNetUsers");
            DropForeignKey("dbo.EventInvites", "EventId", "dbo.Events");
            DropIndex("dbo.EventInvites", new[] { "EventId" });
            DropIndex("dbo.EventInvites", new[] { "SenderId" });
            DropIndex("dbo.EventInvites", new[] { "InvitedId" });
            DropTable("dbo.EventInvites");
        }
    }
}
