namespace Transpo.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class precisions_1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CriticalPoints", "Latitude", c => c.Decimal(nullable: false, precision: 10, scale: 5));
            AlterColumn("dbo.CriticalPoints", "Longitude", c => c.Decimal(nullable: false, precision: 10, scale: 5));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CriticalPoints", "Longitude", c => c.Decimal(nullable: false, precision: 13, scale: 10));
            AlterColumn("dbo.CriticalPoints", "Latitude", c => c.Decimal(nullable: false, precision: 13, scale: 10));
        }
    }
}
