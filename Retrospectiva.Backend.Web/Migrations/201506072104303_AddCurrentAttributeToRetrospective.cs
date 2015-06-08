namespace Retrospectiva.Backend.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCurrentAttributeToRetrospective : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SprintRetrospectives", "Current", c => c.Boolean(nullable: false, defaultValue: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SprintRetrospectives", "Current");
        }
    }
}
