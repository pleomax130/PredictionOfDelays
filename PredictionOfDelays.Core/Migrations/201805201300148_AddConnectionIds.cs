namespace PredictionOfDelays.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddConnectionIds : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ConnectionIdsAsString", c => c.String());
            DropColumn("dbo.UserEvents", "ConnectionIdsAsString");
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "ConnectionIdsAsString");
        }
    }
}
