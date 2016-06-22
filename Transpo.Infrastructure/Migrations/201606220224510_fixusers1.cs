namespace Transpo.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixusers1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "UserInfo_id", "dbo.Users");
            RenameColumn(table: "dbo.AspNetUsers", name: "UserInfo_id", newName: "User_id");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_UserInfo_id", newName: "IX_User_id");
            AddForeignKey("dbo.AspNetUsers", "User_id", "dbo.Users", "id", cascadeDelete: true);
            DropColumn("dbo.Users", "AppUserId");
            DropColumn("dbo.AspNetUsers", "UserInfoId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "UserInfoId", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "AppUserId", c => c.String());
            DropForeignKey("dbo.AspNetUsers", "User_id", "dbo.Users");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_User_id", newName: "IX_UserInfo_id");
            RenameColumn(table: "dbo.AspNetUsers", name: "User_id", newName: "UserInfo_id");
            AddForeignKey("dbo.AspNetUsers", "UserInfo_id", "dbo.Users", "id");
        }
    }
}
