namespace Bil.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNumberofwheels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Bils", "NumberOfWheels", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Bils", "NumberOfWheels");
        }
    }
}
