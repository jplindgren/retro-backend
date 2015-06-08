using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Retrospectiva.Backend.Web.Models {
    public class Sprint {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public int Number { get; set; }

        public virtual ICollection<SprintRetrospective> Retrospectives { get; set; }

        public void AddRetrospectiveFor(Team team) {
            if (Retrospectives.Any(x => x.TeamId == team.Id)) 
                throw new Exception("Retrospective already created!");        

            var retrospective = new SprintRetrospective() {
                SprintId = this.Id,
                TeamId = team.Id
            };
            retrospective.SetRetrospectiveMembers(team.Members.ToList());
            Retrospectives.Add(retrospective);
        }
    } //class
}