namespace Transpo.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "PictureUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "PictureUrl");
        }
    }
}
