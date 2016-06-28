namespace Transpo.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nonrequired : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "User_id", "dbo.Users");
            DropIndex("dbo.AspNetUsers", new[] { "User_id" });
            AlterColumn("dbo.AspNetUsers", "User_id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "User_id");
            AddForeignKey("dbo.AspNetUsers", "User_id", "dbo.Users", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "User_id", "dbo.Users");
            DropIndex("dbo.AspNetUsers", new[] { "User_id" });
            AlterColumn("dbo.AspNetUsers", "User_id", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "User_id");
            AddForeignKey("dbo.AspNetUsers", "User_id", "dbo.Users", "id", cascadeDelete: true);
        }
    }
}
