namespace PredictionOfDelays.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Localizationv2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "Localization_LocalizationId", "dbo.Localizations");
            DropIndex("dbo.Events", new[] { "Localization_LocalizationId" });
            AlterColumn("dbo.Events", "Localization_LocalizationId", c => c.Int(nullable: false));
            CreateIndex("dbo.Events", "Localization_LocalizationId");
            AddForeignKey("dbo.Events", "Localization_LocalizationId", "dbo.Localizations", "LocalizationId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Events", "Localization_LocalizationId", "dbo.Localizations");
            DropIndex("dbo.Events", new[] { "Localization_LocalizationId" });
            AlterColumn("dbo.Events", "Localization_LocalizationId", c => c.Int());
            CreateIndex("dbo.Events", "Localization_LocalizationId");
            AddForeignKey("dbo.Events", "Localization_LocalizationId", "dbo.Localizations", "LocalizationId");
        }
    }
}
