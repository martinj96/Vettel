namespace Transpo.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class First : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        id = c.Int(nullable: false),
                        Comfort = c.Int(nullable: false),
                        Color = c.String(),
                        Brand = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Users", t => t.id)
                .Index(t => t.id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Gender = c.Int(nullable: false),
                        Link = c.String(),
                        FacebookId = c.Long(nullable: false),
                        Rating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Email = c.String(),
                        Phone = c.String(),
                        PictureUrl = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        ReviewerId = c.Int(nullable: false),
                        RevieweeId = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Users", t => t.ReviewerId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.RevieweeId)
                .Index(t => t.ReviewerId)
                .Index(t => t.RevieweeId);
            
            CreateTable(
                "dbo.Rides",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        PricePerPassenger = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SeatsLeft = c.Int(nullable: false),
                        Length = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MinPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MaxPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Detour = c.Int(nullable: false),
                        Departure = c.DateTime(nullable: false),
                        Description = c.String(),
                        DriverId = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Users", t => t.DriverId)
                .Index(t => t.DriverId);
            
            CreateTable(
                "dbo.OrderedCriticalPoints",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Order = c.Int(nullable: false),
                        RideId = c.Int(nullable: false),
                        CriticalPointId = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.CriticalPoints", t => t.CriticalPointId)
                .ForeignKey("dbo.Rides", t => t.RideId)
                .Index(t => t.RideId)
                .Index(t => t.CriticalPointId);
            
            CreateTable(
                "dbo.CriticalPoints",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Latitude = c.Decimal(nullable: false, precision: 10, scale: 5),
                        Longitude = c.Decimal(nullable: false, precision: 10, scale: 5),
                        Name = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.UserWithAccessRide",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RideId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RideId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Rides", t => t.RideId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RideId);
            
            CreateTable(
                "dbo.UserRide",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RideId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RideId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Rides", t => t.RideId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RideId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRide", "RideId", "dbo.Rides");
            DropForeignKey("dbo.UserRide", "UserId", "dbo.Users");
            DropForeignKey("dbo.Reviews", "RevieweeId", "dbo.Users");
            DropForeignKey("dbo.Rides", "DriverId", "dbo.Users");
            DropForeignKey("dbo.UserWithAccessRide", "RideId", "dbo.Rides");
            DropForeignKey("dbo.UserWithAccessRide", "UserId", "dbo.Users");
            DropForeignKey("dbo.OrderedCriticalPoints", "RideId", "dbo.Rides");
            DropForeignKey("dbo.OrderedCriticalPoints", "CriticalPointId", "dbo.CriticalPoints");
            DropForeignKey("dbo.Reviews", "ReviewerId", "dbo.Users");
            DropForeignKey("dbo.Cars", "id", "dbo.Users");
            DropIndex("dbo.UserRide", new[] { "RideId" });
            DropIndex("dbo.UserRide", new[] { "UserId" });
            DropIndex("dbo.UserWithAccessRide", new[] { "RideId" });
            DropIndex("dbo.UserWithAccessRide", new[] { "UserId" });
            DropIndex("dbo.OrderedCriticalPoints", new[] { "CriticalPointId" });
            DropIndex("dbo.OrderedCriticalPoints", new[] { "RideId" });
            DropIndex("dbo.Rides", new[] { "DriverId" });
            DropIndex("dbo.Reviews", new[] { "RevieweeId" });
            DropIndex("dbo.Reviews", new[] { "ReviewerId" });
            DropIndex("dbo.Cars", new[] { "id" });
            DropTable("dbo.UserRide");
            DropTable("dbo.UserWithAccessRide");
            DropTable("dbo.CriticalPoints");
            DropTable("dbo.OrderedCriticalPoints");
            DropTable("dbo.Rides");
            DropTable("dbo.Reviews");
            DropTable("dbo.Users");
            DropTable("dbo.Cars");
        }
    }
}
