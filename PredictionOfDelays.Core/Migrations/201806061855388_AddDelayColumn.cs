namespace PredictionOfDelays.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDelayColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserEvents", "MinutesOfDelay", c => c.Int(nullable: false));
            DropColumn("dbo.UserEvents", "PlannedArrival");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserEvents", "PlannedArrival", c => c.DateTime(nullable: false));
            DropColumn("dbo.UserEvents", "MinutesOfDelay");
        }
    }
}
