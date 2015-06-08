namespace Retrospectiva.Backend.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixRetrospectiveMember : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SprintRetrospectives", "RetrospectiveMember_Id", "dbo.RetrospectiveMembers");
            DropIndex("dbo.SprintRetrospectives", new[] { "RetrospectiveMember_Id" });
            DropColumn("dbo.SprintRetrospectives", "RetrospectiveMember_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SprintRetrospectives", "RetrospectiveMember_Id", c => c.Guid());
            CreateIndex("dbo.SprintRetrospectives", "RetrospectiveMember_Id");
            AddForeignKey("dbo.SprintRetrospectives", "RetrospectiveMember_Id", "dbo.RetrospectiveMembers", "Id");
        }
    }
}
