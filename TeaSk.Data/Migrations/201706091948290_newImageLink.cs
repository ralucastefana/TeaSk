namespace TeaSk.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newImageLink : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activities", "Minutes", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Activities", "Minutes");
        }
    }
}
