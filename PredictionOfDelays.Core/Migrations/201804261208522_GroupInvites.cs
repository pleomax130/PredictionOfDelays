namespace PredictionOfDelays.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GroupInvites : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserGroupInvites",
                c => new
                    {
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUserId, t.GroupId })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.GroupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserGroupInvites", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.UserGroupInvites", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.UserGroupInvites", new[] { "GroupId" });
            DropIndex("dbo.UserGroupInvites", new[] { "ApplicationUserId" });
            DropTable("dbo.UserGroupInvites");
        }
    }
}
