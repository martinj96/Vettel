namespace Transpo.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RideCriticalPoints", "Ride_id", "dbo.Rides");
            DropForeignKey("dbo.RideCriticalPoints", "CriticalPoint_id", "dbo.CriticalPoints");
            DropIndex("dbo.RideCriticalPoints", new[] { "Ride_id" });
            DropIndex("dbo.RideCriticalPoints", new[] { "CriticalPoint_id" });
            CreateTable(
                "dbo.OrderedCriticalPoints",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Order = c.Int(nullable: false),
                        CriticalPoint_id = c.Int(),
                        Ride_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.CriticalPoints", t => t.CriticalPoint_id)
                .ForeignKey("dbo.Rides", t => t.Ride_id)
                .Index(t => t.CriticalPoint_id)
                .Index(t => t.Ride_id);
            
            DropTable("dbo.RideCriticalPoints");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.RideCriticalPoints",
                c => new
                    {
                        Ride_id = c.Int(nullable: false),
                        CriticalPoint_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Ride_id, t.CriticalPoint_id });
            
            DropForeignKey("dbo.OrderedCriticalPoints", "Ride_id", "dbo.Rides");
            DropForeignKey("dbo.OrderedCriticalPoints", "CriticalPoint_id", "dbo.CriticalPoints");
            DropIndex("dbo.OrderedCriticalPoints", new[] { "Ride_id" });
            DropIndex("dbo.OrderedCriticalPoints", new[] { "CriticalPoint_id" });
            DropTable("dbo.OrderedCriticalPoints");
            CreateIndex("dbo.RideCriticalPoints", "CriticalPoint_id");
            CreateIndex("dbo.RideCriticalPoints", "Ride_id");
            AddForeignKey("dbo.RideCriticalPoints", "CriticalPoint_id", "dbo.CriticalPoints", "id", cascadeDelete: true);
            AddForeignKey("dbo.RideCriticalPoints", "Ride_id", "dbo.Rides", "id", cascadeDelete: true);
        }
    }
}
