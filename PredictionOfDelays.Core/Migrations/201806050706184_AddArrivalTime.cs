namespace PredictionOfDelays.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddArrivalTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserEvents", "PlannedArrival", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserEvents", "PlannedArrival");
        }
    }
}
