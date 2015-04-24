using Retrospectiva.Backend.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Retrospectiva.Backend.Web.Representation {
    public class ModelFactory {
        public MemberRepresentation GetMemberRepresentation(Member member) {
            return new MemberRepresentation() {
                Id = member.Id,
                Name = member.Name,
                TeamId = member.TeamId,
                TeamName = member.Team.Name
            };
        }
    } //class
}