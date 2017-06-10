namespace TeaSk.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addednewcolumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Activities", "Skill_Id", c => c.Int());
            CreateIndex("dbo.Activities", "Skill_Id");
            AddForeignKey("dbo.Activities", "Skill_Id", "dbo.Skills", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Activities", "Skill_Id", "dbo.Skills");
            DropIndex("dbo.Activities", new[] { "Skill_Id" });
            DropColumn("dbo.Activities", "Skill_Id");
        }
    }
}
