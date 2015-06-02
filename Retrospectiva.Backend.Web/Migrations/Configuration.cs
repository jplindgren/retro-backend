namespace Retrospectiva.Backend.Web.Migrations
{
    using Retrospectiva.Backend.Web.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Retrospectiva.Backend.Web.Repository.RetroContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Retrospectiva.Backend.Web.Repository.RetroContext";
        }

        protected override void Seed(Retrospectiva.Backend.Web.Repository.RetroContext context){
            for (int i = 1; i < 5; i++) {
                context.Sprints.AddOrUpdate(new Sprint() {
                    Number = i
                });   
            }

            context.SaveChanges();

            context.Teams.AddOrUpdate(new Team() { 
                Name = "DropEvents"
            });

            context.Teams.AddOrUpdate(new Team() {
                Name = "Stark"
            });

            context.Teams.AddOrUpdate(new Team() {
                Name = "RM"
            });

            context.SaveChanges();
        }
    }
}
