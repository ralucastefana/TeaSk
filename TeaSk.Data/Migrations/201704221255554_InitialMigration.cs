namespace TeaSk.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Levels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Limit = c.String(),
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
                "dbo.UserSkills",
                c => new
                    {
                        User_Id = c.Int(nullable: false),
                        Skills_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_Id, t.Skills_Id })
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .ForeignKey("dbo.Skills", t => t.Skills_Id, cascadeDelete: true)
                .Index(t => t.User_Id)
                .Index(t => t.Skills_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserSkills", "Skills_Id", "dbo.Skills");
            DropForeignKey("dbo.UserSkills", "User_Id", "dbo.Users");
            DropIndex("dbo.UserSkills", new[] { "Skills_Id" });
            DropIndex("dbo.UserSkills", new[] { "User_Id" });
            DropTable("dbo.UserSkills");
            DropTable("dbo.Users");
            DropTable("dbo.Skills");
            DropTable("dbo.Levels");
            DropTable("dbo.Activities");
        }
    }
}
