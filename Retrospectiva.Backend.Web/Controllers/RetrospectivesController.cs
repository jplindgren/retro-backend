using Retrospectiva.Backend.Web.Models;
using Retrospectiva.Backend.Web.Representation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Retrospectiva.Backend.Web.Controllers {
    [RoutePrefix("api")]
    public class RetrospectivesController : BaseApiController {
        // GET api/values/5
        [Route("retrospectives/{id:Guid}")]
        public RetrospectiveDetailRepresentation Get(Guid id) {
            return ModelFactory.GetRetrospectiveDetailRepresentation(GetRetrospective(id));
        }

        // GET api/teams/4/values
        [Route("teams/{id:Guid}/retrospectives")]
        public IEnumerable<RetrospectiveRepresentation> GetByTeamId(Guid id) {
            return Context.Retrospectives.Include("Team").Include("Sprint")
                .Where(x => x.TeamId == id).ToList()
                .Select(x => ModelFactory.GetRetrospectiveRepresentation(x)).ToList();
        }

        // POST api/values
        [HttpPost]
        [Route("sprints/{id:Guid}/retrospectives")]
        public void Post(Guid id, [FromBody]CreateRetrospetiveMessageDTO value) {
            var sprint = Context.Sprints.Where(x => x.Id == id).FirstOrDefault();
            var team = Context.Teams.Where(x => x.Id == value.TeamId).FirstOrDefault();
            sprint.AddRetrospectiveFor(team);

            Context.SaveChanges();
        }

        #region QUESTIONS
        //[HttpPost()]
        //[Route("{sprintId:Guid}/questions")]        
        //public void AddQuestions([FromBody]Question value, Guid sprintId) {
        //    var sprint = GetRetrospective(sprintId);
        //    sprint.Questions.Add(value);
        //    Context.SaveChanges();
        //}

        //// POST api/values
        //[Route("{sprintId:Guid}/questions/{questionId}")]
        //[HttpDelete]
        //public void RemoveQuestions(Guid sprintId, Guid questionId) {
        //    var sprint = GetRetrospective(sprintId);
        //    var targetQuestion = sprint.Questions.Where(x => x.Id == questionId).FirstOrDefault();
        //    sprint.Questions.Remove(targetQuestion);
        //    Context.SaveChanges();
        //}

        #endregion

        private SprintRetrospective GetRetrospective(Guid id) {
            return Context.Retrospectives.Include("Sprint").Include("Team").Where(x => x.Id == id).FirstOrDefault();
        }
    } //class

    public class CreateRetrospetiveMessageDTO {
        public Guid TeamId { get; set; }
    }
}