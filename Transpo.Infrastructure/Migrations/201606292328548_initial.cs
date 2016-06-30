namespace Transpo.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
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
                        Gender = c.Int(),
                        Age = c.Int(),
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
                        PricePerPassenger = c.Decimal(precision: 18, scale: 2),
                        SeatsLeft = c.Int(),
                        Length = c.Decimal(precision: 18, scale: 2),
                        MinPrice = c.Decimal(precision: 18, scale: 2),
                        MaxPrice = c.Decimal(precision: 18, scale: 2),
                        Detour = c.Int(),
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
                "dbo.Messages",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Subject = c.String(),
                        MessageBody = c.String(),
                        IsRead = c.Boolean(nullable: false),
                        SenderId = c.Int(nullable: false),
                        RecipientId = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Users", t => t.RecipientId)
                .ForeignKey("dbo.Users", t => t.SenderId)
                .Index(t => t.SenderId)
                .Index(t => t.RecipientId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        User_id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.User_id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
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
            DropForeignKey("dbo.AspNetUsers", "User_id", "dbo.Users");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.UserRide", "RideId", "dbo.Rides");
            DropForeignKey("dbo.UserRide", "UserId", "dbo.Users");
            DropForeignKey("dbo.Reviews", "RevieweeId", "dbo.Users");
            DropForeignKey("dbo.Messages", "SenderId", "dbo.Users");
            DropForeignKey("dbo.Messages", "RecipientId", "dbo.Users");
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
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "User_id" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Messages", new[] { "RecipientId" });
            DropIndex("dbo.Messages", new[] { "SenderId" });
            DropIndex("dbo.OrderedCriticalPoints", new[] { "CriticalPointId" });
            DropIndex("dbo.OrderedCriticalPoints", new[] { "RideId" });
            DropIndex("dbo.Rides", new[] { "DriverId" });
            DropIndex("dbo.Reviews", new[] { "RevieweeId" });
            DropIndex("dbo.Reviews", new[] { "ReviewerId" });
            DropIndex("dbo.Cars", new[] { "id" });
            DropTable("dbo.UserRide");
            DropTable("dbo.UserWithAccessRide");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Messages");
            DropTable("dbo.CriticalPoints");
            DropTable("dbo.OrderedCriticalPoints");
            DropTable("dbo.Rides");
            DropTable("dbo.Reviews");
            DropTable("dbo.Users");
            DropTable("dbo.Cars");
        }
    }
}
