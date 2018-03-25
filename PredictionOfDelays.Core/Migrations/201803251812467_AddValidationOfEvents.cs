namespace PredictionOfDelays.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddValidationOfEvents : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Events", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Events", "Description", c => c.String(maxLength: 250));
            AlterColumn("dbo.Groups", "Name", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.Groups", "Description", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Groups", "Description", c => c.String());
            AlterColumn("dbo.Groups", "Name", c => c.String());
            AlterColumn("dbo.Events", "Description", c => c.String());
            AlterColumn("dbo.Events", "Name", c => c.String());
        }
    }
}
