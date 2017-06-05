namespace TeaSk.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewDBSetup : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activities", "Image", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Activities", "Image");
        }
    }
}
