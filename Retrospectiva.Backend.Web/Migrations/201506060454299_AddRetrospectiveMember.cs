namespace Retrospectiva.Backend.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRetrospectiveMember : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SprintRetrospectiveMembers", "SprintRetrospective_Id", "dbo.SprintRetrospectives");
            DropForeignKey("dbo.SprintRetrospectiveMembers", "Member_Id", "dbo.Members");
            DropIndex("dbo.SprintRetrospectiveMembers", new[] { "SprintRetrospective_Id" });
            DropIndex("dbo.SprintRetrospectiveMembers", new[] { "Member_Id" });
            CreateTable(
                "dbo.RetrospectiveMembers",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        RetrospectiveId = c.Guid(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .ForeignKey("dbo.SprintRetrospectives", t => t.RetrospectiveId, cascadeDelete: true)
                .Index(t => t.RetrospectiveId)
                .Index(t => t.UserId);
            
            AddColumn("dbo.Answers", "RetrospectiveMember_Id", c => c.Guid());
            AddColumn("dbo.SprintRetrospectives", "RetrospectiveMember_Id", c => c.Guid());
            CreateIndex("dbo.Answers", "RetrospectiveMember_Id");
            CreateIndex("dbo.SprintRetrospectives", "RetrospectiveMember_Id");
            AddForeignKey("dbo.Answers", "RetrospectiveMember_Id", "dbo.RetrospectiveMembers", "Id");
            AddForeignKey("dbo.SprintRetrospectives", "RetrospectiveMember_Id", "dbo.RetrospectiveMembers", "Id");
            DropTable("dbo.SprintRetrospectiveMembers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SprintRetrospectiveMembers",
                c => new
                    {
                        SprintRetrospective_Id = c.Guid(nullable: false),
                        Member_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.SprintRetrospective_Id, t.Member_Id });
            
            DropForeignKey("dbo.RetrospectiveMembers", "RetrospectiveId", "dbo.SprintRetrospectives");
            DropForeignKey("dbo.RetrospectiveMembers", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.SprintRetrospectives", "RetrospectiveMember_Id", "dbo.RetrospectiveMembers");
            DropForeignKey("dbo.Answers", "RetrospectiveMember_Id", "dbo.RetrospectiveMembers");
            DropIndex("dbo.RetrospectiveMembers", new[] { "UserId" });
            DropIndex("dbo.RetrospectiveMembers", new[] { "RetrospectiveId" });
            DropIndex("dbo.SprintRetrospectives", new[] { "RetrospectiveMember_Id" });
            DropIndex("dbo.Answers", new[] { "RetrospectiveMember_Id" });
            DropColumn("dbo.SprintRetrospectives", "RetrospectiveMember_Id");
            DropColumn("dbo.Answers", "RetrospectiveMember_Id");
            DropTable("dbo.RetrospectiveMembers");
            CreateIndex("dbo.SprintRetrospectiveMembers", "Member_Id");
            CreateIndex("dbo.SprintRetrospectiveMembers", "SprintRetrospective_Id");
            AddForeignKey("dbo.SprintRetrospectiveMembers", "Member_Id", "dbo.Members", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SprintRetrospectiveMembers", "SprintRetrospective_Id", "dbo.SprintRetrospectives", "Id", cascadeDelete: true);
        }
    }
}
