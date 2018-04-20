namespace PredictionOfDelays.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Rename : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Events", name: "ApplicationUser_Id", newName: "OwnerId");
            RenameColumn(table: "dbo.Groups", name: "ApplicationUser_Id", newName: "OwnerId");
            RenameIndex(table: "dbo.Events", name: "IX_ApplicationUser_Id", newName: "IX_OwnerId");
            RenameIndex(table: "dbo.Groups", name: "IX_ApplicationUser_Id", newName: "IX_OwnerId");
            DropColumn("dbo.Events", "OwnerUserId");
            DropColumn("dbo.Groups", "OwnerUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Groups", "OwnerUserId", c => c.String());
            AddColumn("dbo.Events", "OwnerUserId", c => c.String());
            RenameIndex(table: "dbo.Groups", name: "IX_OwnerId", newName: "IX_ApplicationUser_Id");
            RenameIndex(table: "dbo.Events", name: "IX_OwnerId", newName: "IX_ApplicationUser_Id");
            RenameColumn(table: "dbo.Groups", name: "OwnerId", newName: "ApplicationUser_Id");
            RenameColumn(table: "dbo.Events", name: "OwnerId", newName: "ApplicationUser_Id");
        }
    }
}
