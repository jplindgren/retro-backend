namespace Retrospectiva.Backend.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSprintToMember : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Members", "SprintId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Members", "SprintId");
            AddForeignKey("dbo.Members", "SprintId", "dbo.Sprints", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Members", "SprintId", "dbo.Sprints");
            DropIndex("dbo.Members", new[] { "SprintId" });
            DropColumn("dbo.Members", "SprintId");
        }
    }
}
