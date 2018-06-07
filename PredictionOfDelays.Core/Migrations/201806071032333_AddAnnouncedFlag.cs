namespace PredictionOfDelays.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAnnouncedFlag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserEvents", "IsAnnounced", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserEvents", "IsAnnounced");
        }
    }
}
