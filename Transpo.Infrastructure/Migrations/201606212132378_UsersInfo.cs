namespace Transpo.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UsersInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "AppUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Reviews", "AppUser_Id1", c => c.String(maxLength: 128));
            AddColumn("dbo.Rides", "AppUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Rides", "AppUser_Id1", c => c.String(maxLength: 128));
            AddColumn("dbo.Rides", "AppUser_Id2", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUsers", "Name", c => c.String());
            AddColumn("dbo.AspNetUsers", "Gender", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "Link", c => c.String());
            AddColumn("dbo.AspNetUsers", "Rating", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.AspNetUsers", "Phone", c => c.String());
            AddColumn("dbo.AspNetUsers", "PictureUrl", c => c.String());
            AddColumn("dbo.AspNetUsers", "Car_id", c => c.Int());
            CreateIndex("dbo.Reviews", "AppUser_Id");
            CreateIndex("dbo.Reviews", "AppUser_Id1");
            CreateIndex("dbo.Rides", "AppUser_Id");
            CreateIndex("dbo.Rides", "AppUser_Id1");
            CreateIndex("dbo.Rides", "AppUser_Id2");
            CreateIndex("dbo.AspNetUsers", "Car_id");
            AddForeignKey("dbo.AspNetUsers", "Car_id", "dbo.Cars", "id");
            AddForeignKey("dbo.Reviews", "AppUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Rides", "AppUser_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Rides", "AppUser_Id1", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Reviews", "AppUser_Id1", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Rides", "AppUser_Id2", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rides", "AppUser_Id2", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reviews", "AppUser_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.Rides", "AppUser_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.Rides", "AppUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Reviews", "AppUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Car_id", "dbo.Cars");
            DropIndex("dbo.AspNetUsers", new[] { "Car_id" });
            DropIndex("dbo.Rides", new[] { "AppUser_Id2" });
            DropIndex("dbo.Rides", new[] { "AppUser_Id1" });
            DropIndex("dbo.Rides", new[] { "AppUser_Id" });
            DropIndex("dbo.Reviews", new[] { "AppUser_Id1" });
            DropIndex("dbo.Reviews", new[] { "AppUser_Id" });
            DropColumn("dbo.AspNetUsers", "Car_id");
            DropColumn("dbo.AspNetUsers", "PictureUrl");
            DropColumn("dbo.AspNetUsers", "Phone");
            DropColumn("dbo.AspNetUsers", "Rating");
            DropColumn("dbo.AspNetUsers", "Link");
            DropColumn("dbo.AspNetUsers", "Gender");
            DropColumn("dbo.AspNetUsers", "Name");
            DropColumn("dbo.Rides", "AppUser_Id2");
            DropColumn("dbo.Rides", "AppUser_Id1");
            DropColumn("dbo.Rides", "AppUser_Id");
            DropColumn("dbo.Reviews", "AppUser_Id1");
            DropColumn("dbo.Reviews", "AppUser_Id");
        }
    }
}
