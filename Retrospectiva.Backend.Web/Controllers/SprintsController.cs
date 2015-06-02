using Retrospectiva.Backend.Web.Filters;
using Retrospectiva.Backend.Web.Models;
using Retrospectiva.Backend.Web.Representation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Retrospectiva.Backend.Web.Controllers {
    [RoutePrefix("api/sprints")]
    [AllowCrossOrigin]
    public class SprintsController : BaseApiController {
        // GET api/values
        [Route("")]
        public IHttpActionResult Get() {
            var sprintsRepresentation = Context.Sprints
                .OrderByDescending(x => x.Number)
                .Select(x => ModelFactory.GetSprintRepresentation(x)).ToList();
            return Ok<IEnumerable<SprintRepresentation>>(sprintsRepresentation);
        }

        // GET api/values/5
        [Route("{id:Guid}")]
        public SprintRepresentation Get(Guid id) {
            return ModelFactory.GetSprintRepresentation(GetSprint(id));
        }

        // POST api/values
        [HttpPost]
        [Route("")]
        public void Post([FromBody]Sprint value) {
            var sprint = new Sprint(){
                Number = value.Number,
            };
            
            Context.Sprints.Add(sprint);
            Context.SaveChanges();
        }

        [HttpOptions]
        [Route("")]
        public HttpResponseMessage Options() {
            var response = new HttpResponseMessage();
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }

        private Sprint GetSprint(Guid id) {
            return Context.Sprints.Include("Retrospectives").Where(x => x.Id == id).FirstOrDefault();
        }
    } //class
}