namespace PredictionOfDelays.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExcepitonTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExceptionWrappers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExceptionMessage = c.String(),
                        ControllerName = c.String(),
                        ExceptionStackTrace = c.String(),
                        LogTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ExceptionWrappers");
        }
    }
}
