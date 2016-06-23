namespace Transpo.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Gender", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Gender", c => c.Int(nullable: false));
        }
    }
}
