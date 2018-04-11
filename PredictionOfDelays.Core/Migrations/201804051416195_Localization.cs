namespace PredictionOfDelays.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Localization : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Localizations",
                c => new
                    {
                        LocalizationId = c.Int(nullable: false, identity: true),
                        Latitude = c.String(),
                        Longitude = c.String(),
                    })
                .PrimaryKey(t => t.LocalizationId);
            
            AddColumn("dbo.Events", "OwnerUserId", c => c.String());
            AddColumn("dbo.Events", "Localization_LocalizationId", c => c.Int());
            AddColumn("dbo.Groups", "OwnerUserId", c => c.String());
            CreateIndex("dbo.Events", "Localization_LocalizationId");
            AddForeignKey("dbo.Events", "Localization_LocalizationId", "dbo.Localizations", "LocalizationId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "Localization_LocalizationId", "dbo.Localizations");
            DropIndex("dbo.Events", new[] { "Localization_LocalizationId" });
            DropColumn("dbo.Groups", "OwnerUserId");
            DropColumn("dbo.Events", "Localization_LocalizationId");
            DropColumn("dbo.Events", "OwnerUserId");
            DropTable("dbo.Localizations");
        }
    }
}
