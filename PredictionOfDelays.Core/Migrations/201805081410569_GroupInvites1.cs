namespace PredictionOfDelays.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GroupInvites1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserGroupInvites", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.UserGroupInvites", "GroupId", "dbo.Groups");
            DropIndex("dbo.UserGroupInvites", new[] { "ApplicationUserId" });
            DropIndex("dbo.UserGroupInvites", new[] { "GroupId" });
            CreateTable(
                "dbo.GroupInvites",
                c => new
                    {
                        GroupInviteId = c.Guid(nullable: false),
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
            
            DropTable("dbo.UserGroupInvites");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserGroupInvites",
                c => new
                    {
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUserId, t.GroupId });
            
            DropForeignKey("dbo.GroupInvites", "SenderId", "dbo.AspNetUsers");
            DropForeignKey("dbo.GroupInvites", "InvitedId", "dbo.AspNetUsers");
            DropForeignKey("dbo.GroupInvites", "GroupId", "dbo.Groups");
            DropIndex("dbo.GroupInvites", new[] { "GroupId" });
            DropIndex("dbo.GroupInvites", new[] { "SenderId" });
            DropIndex("dbo.GroupInvites", new[] { "InvitedId" });
            DropTable("dbo.GroupInvites");
            CreateIndex("dbo.UserGroupInvites", "GroupId");
            CreateIndex("dbo.UserGroupInvites", "ApplicationUserId");
            AddForeignKey("dbo.UserGroupInvites", "GroupId", "dbo.Groups", "GroupId", cascadeDelete: true);
            AddForeignKey("dbo.UserGroupInvites", "ApplicationUserId", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
