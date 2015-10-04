namespace Transpo.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Comfort = c.Int(nullable: false),
                        Color = c.String(),
                        Brand = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                        User_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Users", t => t.User_id)
                .Index(t => t.User_id);
            
            CreateTable(
                "dbo.Characteristics",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                        User_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Users", t => t.User_id)
                .Index(t => t.User_id);
            
            CreateTable(
                "dbo.CriticalPoints",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Latitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Longitude = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Name = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
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
                        DateCreated = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                        Driver_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Users", t => t.Driver_id)
                .Index(t => t.Driver_id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Gender = c.Int(nullable: false),
                        Rating = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Email = c.String(),
                        Phone = c.String(),
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
                        DateCreated = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                        Reviewee_id = c.Int(),
                        Reviewer_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Users", t => t.Reviewee_id)
                .ForeignKey("dbo.Users", t => t.Reviewer_id)
                .Index(t => t.Reviewee_id)
                .Index(t => t.Reviewer_id);
            
            CreateTable(
                "dbo.RideCriticalPoints",
                c => new
                    {
                        Ride_id = c.Int(nullable: false),
                        CriticalPoint_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Ride_id, t.CriticalPoint_id })
                .ForeignKey("dbo.Rides", t => t.Ride_id, cascadeDelete: true)
                .ForeignKey("dbo.CriticalPoints", t => t.CriticalPoint_id, cascadeDelete: true)
                .Index(t => t.Ride_id)
                .Index(t => t.CriticalPoint_id);
            
            CreateTable(
                "dbo.UserRides",
                c => new
                    {
                        User_id = c.Int(nullable: false),
                        Ride_id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_id, t.Ride_id })
                .ForeignKey("dbo.Users", t => t.User_id, cascadeDelete: true)
                .ForeignKey("dbo.Rides", t => t.Ride_id, cascadeDelete: true)
                .Index(t => t.User_id)
                .Index(t => t.Ride_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rides", "Driver_id", "dbo.Users");
            DropForeignKey("dbo.UserRides", "Ride_id", "dbo.Rides");
            DropForeignKey("dbo.UserRides", "User_id", "dbo.Users");
            DropForeignKey("dbo.Reviews", "Reviewer_id", "dbo.Users");
            DropForeignKey("dbo.Reviews", "Reviewee_id", "dbo.Users");
            DropForeignKey("dbo.Characteristics", "User_id", "dbo.Users");
            DropForeignKey("dbo.Cars", "User_id", "dbo.Users");
            DropForeignKey("dbo.RideCriticalPoints", "CriticalPoint_id", "dbo.CriticalPoints");
            DropForeignKey("dbo.RideCriticalPoints", "Ride_id", "dbo.Rides");
            DropIndex("dbo.UserRides", new[] { "Ride_id" });
            DropIndex("dbo.UserRides", new[] { "User_id" });
            DropIndex("dbo.RideCriticalPoints", new[] { "CriticalPoint_id" });
            DropIndex("dbo.RideCriticalPoints", new[] { "Ride_id" });
            DropIndex("dbo.Reviews", new[] { "Reviewer_id" });
            DropIndex("dbo.Reviews", new[] { "Reviewee_id" });
            DropIndex("dbo.Rides", new[] { "Driver_id" });
            DropIndex("dbo.Characteristics", new[] { "User_id" });
            DropIndex("dbo.Cars", new[] { "User_id" });
            DropTable("dbo.UserRides");
            DropTable("dbo.RideCriticalPoints");
            DropTable("dbo.Reviews");
            DropTable("dbo.Users");
            DropTable("dbo.Rides");
            DropTable("dbo.CriticalPoints");
            DropTable("dbo.Characteristics");
            DropTable("dbo.Cars");
        }
    }
}
