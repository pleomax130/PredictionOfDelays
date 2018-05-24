namespace PredictionOfDelays.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAttribute : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.EventInvites");
            DropPrimaryKey("dbo.GroupInvites");
            AlterColumn("dbo.EventInvites", "EventInviteId", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.GroupInvites", "GroupInviteId", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.EventInvites", "EventInviteId");
            AddPrimaryKey("dbo.GroupInvites", "GroupInviteId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.GroupInvites");
            DropPrimaryKey("dbo.EventInvites");
            AlterColumn("dbo.GroupInvites", "GroupInviteId", c => c.Guid(nullable: false));
            AlterColumn("dbo.EventInvites", "EventInviteId", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.GroupInvites", "GroupInviteId");
            AddPrimaryKey("dbo.EventInvites", "EventInviteId");
        }
    }
}
