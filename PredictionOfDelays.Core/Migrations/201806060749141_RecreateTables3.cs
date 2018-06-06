namespace PredictionOfDelays.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RecreateTables3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.EventInvites",
                    c => new
                    {
                        EventInviteId = c.Int(nullable: false, identity: true),
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

            CreateTable(
                    "dbo.GroupInvites",
                    c => new
                    {
                        GroupInviteId = c.Int(nullable: false, identity: true),
                        InvitedId = c.String(maxLength: 128),
                        SenderId = c.String(maxLength: 128),
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GroupInviteId)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.InvitedId)
                .ForeignKey("dbo.AspNetUsers", t => t.SenderId)
                .Index(t => t.InvitedId)
                .Index(t => t.SenderId)
                .Index(t => t.GroupId);
        }

        public override void Down()
        {
        }
    }
}
