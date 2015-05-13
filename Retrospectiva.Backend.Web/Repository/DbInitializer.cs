using Retrospectiva.Backend.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Retrospectiva.Backend.Web.Repository {
    public class DbInitializer : DropCreateDatabaseAlways<RetroContext>{
        protected override void Seed(RetroContext context) {
            base.Seed(context);
            //IList<Team> teams = new List<Team>();
            //IList<Member> members = new List<Member>();

            //teams.Add(new Team() { Name = "Team DropEvents" });
            //members.Add(new Member() { Name = "Joao Paulo", Team = teams.First() });
            //foreach (Team team in teams)
            //    context.Teams.Add(team);
            context.SaveChanges();
        }
    }
}