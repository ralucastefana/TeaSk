namespace TeaSk.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        Phone = c.String(),
                        Name = c.String(),
                        Surname = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        Email = c.String(),
                        Points = c.Int(nullable: false),
                        Level = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Skills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SkillsUsers",
                c => new
                    {
                        Skills_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Skills_Id, t.User_Id })
                .ForeignKey("dbo.Skills", t => t.Skills_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Skills_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SkillsUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.SkillsUsers", "Skills_Id", "dbo.Skills");
            DropIndex("dbo.SkillsUsers", new[] { "User_Id" });
            DropIndex("dbo.SkillsUsers", new[] { "Skills_Id" });
            DropTable("dbo.SkillsUsers");
            DropTable("dbo.Skills");
            DropTable("dbo.Users");
        }
    }
}
