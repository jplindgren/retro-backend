namespace Retrospectiva.Backend.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                        TeamId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: true)
                .Index(t => t.TeamId);
            
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Text = c.String(),
                        QuestionId = c.Guid(nullable: false),
                        MemberId = c.Guid(nullable: false),
                        RetrospectiveId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Members", t => t.MemberId, cascadeDelete: true)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .ForeignKey("dbo.SprintRetrospectives", t => t.RetrospectiveId, cascadeDelete: true)
                .Index(t => t.QuestionId)
                .Index(t => t.MemberId)
                .Index(t => t.RetrospectiveId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Description = c.String(),
                        RetrospectiveId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SprintRetrospectives", t => t.RetrospectiveId, cascadeDelete: false)
                .Index(t => t.RetrospectiveId);
            
            CreateTable(
                "dbo.SprintRetrospectives",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        SprintId = c.Guid(nullable: false),
                        TeamId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sprints", t => t.SprintId, cascadeDelete: false)
                .ForeignKey("dbo.Teams", t => t.TeamId, cascadeDelete: false)
                .Index(t => t.SprintId)
                .Index(t => t.TeamId);
            
            CreateTable(
                "dbo.Sprints",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Teams",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SprintRetrospectiveMembers",
                c => new
                    {
                        SprintRetrospective_Id = c.Guid(nullable: false),
                        Member_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.SprintRetrospective_Id, t.Member_Id })
                .ForeignKey("dbo.SprintRetrospectives", t => t.SprintRetrospective_Id, cascadeDelete: true)
                .ForeignKey("dbo.Members", t => t.Member_Id, cascadeDelete: true)
                .Index(t => t.SprintRetrospective_Id)
                .Index(t => t.Member_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SprintRetrospectives", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.Members", "TeamId", "dbo.Teams");
            DropForeignKey("dbo.SprintRetrospectives", "SprintId", "dbo.Sprints");
            DropForeignKey("dbo.Questions", "RetrospectiveId", "dbo.SprintRetrospectives");
            DropForeignKey("dbo.SprintRetrospectiveMembers", "Member_Id", "dbo.Members");
            DropForeignKey("dbo.SprintRetrospectiveMembers", "SprintRetrospective_Id", "dbo.SprintRetrospectives");
            DropForeignKey("dbo.Answers", "RetrospectiveId", "dbo.SprintRetrospectives");
            DropForeignKey("dbo.Answers", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.Answers", "MemberId", "dbo.Members");
            DropIndex("dbo.SprintRetrospectiveMembers", new[] { "Member_Id" });
            DropIndex("dbo.SprintRetrospectiveMembers", new[] { "SprintRetrospective_Id" });
            DropIndex("dbo.SprintRetrospectives", new[] { "TeamId" });
            DropIndex("dbo.SprintRetrospectives", new[] { "SprintId" });
            DropIndex("dbo.Questions", new[] { "RetrospectiveId" });
            DropIndex("dbo.Answers", new[] { "RetrospectiveId" });
            DropIndex("dbo.Answers", new[] { "MemberId" });
            DropIndex("dbo.Answers", new[] { "QuestionId" });
            DropIndex("dbo.Members", new[] { "TeamId" });
            DropTable("dbo.SprintRetrospectiveMembers");
            DropTable("dbo.Teams");
            DropTable("dbo.Sprints");
            DropTable("dbo.SprintRetrospectives");
            DropTable("dbo.Questions");
            DropTable("dbo.Answers");
            DropTable("dbo.Members");
        }
    }
}
