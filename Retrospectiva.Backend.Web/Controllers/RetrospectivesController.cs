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
        public IHttpActionResult GetByTeamId(Guid id) {
            var retrospectives = Context.Retrospectives.Include("Team").Include("Sprint").Include("Members.User").Include("Questions")
                .Where(x => x.TeamId == id)
                .OrderByDescending(x => x.Sprint.Number)
                .Take(5)
                .ToList()
                .Select(x => ModelFactory.GetRetrospectiveDetailRepresentation(x));
            return Ok<IEnumerable<RetrospectiveDetailRepresentation>>(retrospectives);
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
        [HttpGet()]
        [Route("retrospectives/{retrospectiveId:Guid}/questions")]
        public IEnumerable<QuestionRepresentation> GetQuestions(Guid retrospectiveId) {            
            return Context.Questions.Include("Retrospective")
                .Where(x => x.RetrospectiveId == retrospectiveId).ToList()
                .Select(x => new QuestionRepresentation() { Description = x.Description, Id = x.Id });
        }

        [HttpPost()]
        [Route("retrospectives/{retrospectiveId:Guid}/questions")]
        public void AddQuestions([FromBody]Question value, Guid retrospectiveId) {
            //var retrospective = GetRetrospective(retrospectiveId);            
            value.RetrospectiveId = retrospectiveId;
            //retrospective.Questions.Add(value);
            Context.Questions.Add(value);
            Context.SaveChanges();
        }

        [Route("retrospectives/{retrospectiveId:Guid}/questions/{questionId}")]
        [HttpDelete]
        public void RemoveQuestions(Guid retrospectiveId, Guid questionId) {
            var retrospective = GetRetrospective(retrospectiveId);
            var targetQuestion = retrospective.Questions.Where(x => x.Id == questionId).FirstOrDefault();
            retrospective.Questions.Remove(targetQuestion);
            Context.SaveChanges();
        }
        #endregion

        private SprintRetrospective GetRetrospective(Guid id) {
            return Context.Retrospectives.Include("Sprint").Include("Team").Where(x => x.Id == id).FirstOrDefault();
        }
    } //class

    public class CreateRetrospetiveMessageDTO {
        public Guid TeamId { get; set; }
    }
}