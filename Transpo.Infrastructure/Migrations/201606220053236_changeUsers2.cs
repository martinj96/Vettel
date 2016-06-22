namespace Transpo.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeUsers2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Car_id", "dbo.Cars");
            DropForeignKey("dbo.Reviews", "AppUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Rides", "AppUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Rides", "AppUser_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reviews", "AppUser_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.Rides", "AppUser_Id2", "dbo.AspNetUsers");
            DropIndex("dbo.Reviews", new[] { "AppUser_Id" });
            DropIndex("dbo.Reviews", new[] { "AppUser_Id1" });
            DropIndex("dbo.Rides", new[] { "AppUser_Id" });
            DropIndex("dbo.Rides", new[] { "AppUser_Id1" });
            DropIndex("dbo.Rides", new[] { "AppUser_Id2" });
            DropIndex("dbo.AspNetUsers", new[] { "Car_id" });
            AddColumn("dbo.Users", "AppUserId", c => c.String());
            AddColumn("dbo.AspNetUsers", "UserInfoId", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "UserInfo_id", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "UserInfo_id");
            AddForeignKey("dbo.AspNetUsers", "UserInfo_id", "dbo.Users", "id");
            DropColumn("dbo.Reviews", "AppUser_Id");
            DropColumn("dbo.Reviews", "AppUser_Id1");
            DropColumn("dbo.Rides", "AppUser_Id");
            DropColumn("dbo.Rides", "AppUser_Id1");
            DropColumn("dbo.Rides", "AppUser_Id2");
            DropColumn("dbo.AspNetUsers", "Gender");
            DropColumn("dbo.AspNetUsers", "Link");
            DropColumn("dbo.AspNetUsers", "Rating");
            DropColumn("dbo.AspNetUsers", "Phone");
            DropColumn("dbo.AspNetUsers", "PictureUrl");
            DropColumn("dbo.AspNetUsers", "Car_id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Car_id", c => c.Int());
            AddColumn("dbo.AspNetUsers", "PictureUrl", c => c.String());
            AddColumn("dbo.AspNetUsers", "Phone", c => c.String());
            AddColumn("dbo.AspNetUsers", "Rating", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.AspNetUsers", "Link", c => c.String());
            AddColumn("dbo.AspNetUsers", "Gender", c => c.Int(nullable: false));
            AddColumn("dbo.Rides", "AppUser_Id2", c => c.String(maxLength: 128));
            AddColumn("dbo.Rides", "AppUser_Id1", c => c.String(maxLength: 128));
            AddColumn("dbo.Rides", "AppUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Reviews", "AppUser_Id1", c => c.String(maxLength: 128));
            AddColumn("dbo.Reviews", "AppUser_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.AspNetUsers", "UserInfo_id", "dbo.Users");
            DropIndex("dbo.AspNetUsers", new[] { "UserInfo_id" });
            DropColumn("dbo.AspNetUsers", "UserInfo_id");
            DropColumn("dbo.AspNetUsers", "UserInfoId");
            DropColumn("dbo.Users", "AppUserId");
            CreateIndex("dbo.AspNetUsers", "Car_id");
            CreateIndex("dbo.Rides", "AppUser_Id2");
            CreateIndex("dbo.Rides", "AppUser_Id1");
            CreateIndex("dbo.Rides", "AppUser_Id");
            CreateIndex("dbo.Reviews", "AppUser_Id1");
            CreateIndex("dbo.Reviews", "AppUser_Id");
            AddForeignKey("dbo.Rides", "AppUser_Id2", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Reviews", "AppUser_Id1", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Rides", "AppUser_Id1", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Rides", "AppUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Reviews", "AppUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.AspNetUsers", "Car_id", "dbo.Cars", "id");
        }
    }
}
