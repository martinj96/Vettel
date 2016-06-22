namespace Transpo.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullables : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Rides", "PricePerPassenger", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Rides", "SeatsLeft", c => c.Int());
            AlterColumn("dbo.Rides", "Length", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Rides", "MinPrice", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Rides", "MaxPrice", c => c.Decimal(precision: 18, scale: 2));
            AlterColumn("dbo.Rides", "Detour", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Rides", "Detour", c => c.Int(nullable: false));
            AlterColumn("dbo.Rides", "MaxPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Rides", "MinPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Rides", "Length", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Rides", "SeatsLeft", c => c.Int(nullable: false));
            AlterColumn("dbo.Rides", "PricePerPassenger", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
