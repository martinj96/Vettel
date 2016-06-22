namespace Transpo.Infrastructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class identityfix : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "MyExtraPropertyForTesting");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "MyExtraPropertyForTesting", c => c.Int(nullable: false));
        }
    }
}
