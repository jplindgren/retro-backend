using Retrospectiva.Backend.Web.Models;
using Retrospectiva.Backend.Web.Representation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Retrospectiva.Backend.Web.Controllers {
    [RoutePrefix("api/teams")]
    public class TeamsController : BaseApiController {
        // POST api/teams
        [Route("")]
        public IHttpActionResult Get() {
            return Ok<IEnumerable<TeamRepresentation>>(Context.Teams.ToList().Select(x => ModelFactory.GetTeamRepresentation(x)));
        }

        // POST api/teams/{teamId:Guid}/members
        [Route("{teamId:Guid}/members")]
        public IHttpActionResult Get(Guid teamId, Guid sprintId) {
            if (Guid.Empty == sprintId)
                return BadRequest("sprintId parameters not passed");
            IEnumerable<MemberRepresentation> membersRepresentation = Context.Members.Include("User").Where(x => x.TeamId == teamId && x.SprintId == sprintId)
                                    .ToList()
                                    .Select(x => ModelFactory.GetMemberRepresentation(x));
            return Ok<IEnumerable<MemberRepresentation>>(membersRepresentation);
        }

        // POST api/members
        [Route("")]
        [HttpPost]
        public void Post([FromBody]Team value) {
            var team = new Team() {
                Name = value.Name
            };
            Context.Teams.Add(team);
            Context.SaveChanges();
        }

        // POST api/members
        [Route("{id:Guid}/member")]
        [HttpPost]
        public void AddMember(Guid id, [FromBody]Member value) {
            var team = Context.Teams.Where(x => x.Id == id).FirstOrDefault();
            var member = Context.Members.Where(x => x.Id == value.Id).FirstOrDefault();
            member.Team = team;
            member.TeamId = id;
            team.Members.Add(member);
            Context.SaveChanges();
        }
    } //class
}