namespace Transpo.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class facebookId : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "FacebookId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "FacebookId", c => c.Long(nullable: false));
        }
    }
}
