namespace Transpo.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "Surname");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Surname", c => c.String());
        }
    }
}
