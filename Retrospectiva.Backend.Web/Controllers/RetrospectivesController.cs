using Retrospectiva.Backend.Web.Models;
using Retrospectiva.Backend.Web.Representation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Retrospectiva.Backend.Web.Controllers {
    [RoutePrefix("api/retrospectives")]
    public class RetrospectivesController : BaseApiController {
        // GET api/values
        [Route("")]
        public IEnumerable<RetrospectiveRepresentation> Get() {
            return Context.Retrospectives.Select(x => ModelFactory.GetRetrospectiveRepresentation(x)).ToList();
        }

        // GET api/values/5
        [Route("{id:Guid}")]
        public RetrospectiveDetailRepresentation Get(Guid id) {
            return ModelFactory.GetRetrospectiveDetailRepresentation(GetRetrospective(id));
        }

        // POST api/values
        [HttpPost]
        [Route("")]
        public void Post([FromBody]CreateRetrospetiveMessageDTO value) {
            var sprint = Context.Sprints.Where(x => x.Id == value.SprintId).FirstOrDefault();
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
        public Guid SprintId { get; set; }
    }
}