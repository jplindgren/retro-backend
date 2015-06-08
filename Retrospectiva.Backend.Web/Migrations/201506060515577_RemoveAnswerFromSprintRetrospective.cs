namespace Retrospectiva.Backend.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveAnswerFromSprintRetrospective : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Answers", "MemberId", "dbo.Members");
            DropForeignKey("dbo.Answers", "RetrospectiveId", "dbo.SprintRetrospectives");
            DropForeignKey("dbo.Answers", "RetrospectiveMember_Id", "dbo.RetrospectiveMembers");
            DropIndex("dbo.Answers", new[] { "MemberId" });
            DropIndex("dbo.Answers", new[] { "RetrospectiveId" });
            DropIndex("dbo.Answers", new[] { "RetrospectiveMember_Id" });
            RenameColumn(table: "dbo.Answers", name: "RetrospectiveMember_Id", newName: "RetrospectiveMemberId");
            AlterColumn("dbo.Answers", "RetrospectiveMemberId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Answers", "RetrospectiveMemberId");
            AddForeignKey("dbo.Answers", "RetrospectiveMemberId", "dbo.RetrospectiveMembers", "Id", cascadeDelete: true);
            DropColumn("dbo.Answers", "MemberId");
            DropColumn("dbo.Answers", "RetrospectiveId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Answers", "RetrospectiveId", c => c.Guid(nullable: false));
            AddColumn("dbo.Answers", "MemberId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.Answers", "RetrospectiveMemberId", "dbo.RetrospectiveMembers");
            DropIndex("dbo.Answers", new[] { "RetrospectiveMemberId" });
            AlterColumn("dbo.Answers", "RetrospectiveMemberId", c => c.Guid());
            RenameColumn(table: "dbo.Answers", name: "RetrospectiveMemberId", newName: "RetrospectiveMember_Id");
            CreateIndex("dbo.Answers", "RetrospectiveMember_Id");
            CreateIndex("dbo.Answers", "RetrospectiveId");
            CreateIndex("dbo.Answers", "MemberId");
            AddForeignKey("dbo.Answers", "RetrospectiveMember_Id", "dbo.RetrospectiveMembers", "Id");
            AddForeignKey("dbo.Answers", "RetrospectiveId", "dbo.SprintRetrospectives", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Answers", "MemberId", "dbo.Members", "Id", cascadeDelete: true);
        }
    }
}
