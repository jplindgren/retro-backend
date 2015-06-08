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

        // POST api/values
        [HttpPut]
        [Route("")]
        public void Put() {
            CreateDropEventsOldMembers();
            CreateDropEventsOldRetrospectives();

            CreateDropEventsNewMember();
            CreateDropEventsNewRetrospective();
        }

        private void CreateDropEventsNewRetrospective() {
            var sprints = Context.Sprints.ToList();
            var dropEvents = Context.Teams.Where(x => x.Name == "DropEvents").FirstOrDefault();
            
            SprintRetrospective sr = new SprintRetrospective() {
                SprintId = sprints.Where(x => x.Number == 4).FirstOrDefault().Id,
                TeamId = dropEvents.Id
            };
            sr.SetRetrospectiveMembers(dropEvents.Members.ToList());
            Context.Retrospectives.Add(sr);
            
            Context.SaveChanges();
        }

        private void CreateDropEventsNewMember() {
            var dropEvents = Context.Teams.Where(x => x.Name == "DropEvents").FirstOrDefault();
            var newDropeventMember = Context.Users.Where(x => new string[] { "joao.lindgren" }.Contains(x.UserName)).FirstOrDefault();

            
            dropEvents.Members.Add(new Member() {
                TeamId = dropEvents.Id,
                UserId = newDropeventMember.Id
            });
            Context.SaveChanges();
        }

        private void CreateDropEventsOldRetrospectives() {
            var sprints = Context.Sprints.ToList();
            var dropEvents = Context.Teams.Where(x => x.Name == "DropEvents").FirstOrDefault();

            for (int i = 1; i < 4; i++) {
                SprintRetrospective sr = new SprintRetrospective() {
                    SprintId = sprints.Where(x => x.Number == i).FirstOrDefault().Id,
                    TeamId = dropEvents.Id
                };
                sr.SetRetrospectiveMembers(dropEvents.Members.ToList());
                Context.Retrospectives.Add(sr);    
            }
            Context.SaveChanges();
        }

        private void CreateDropEventsOldMembers() {            
            var dropEvents = Context.Teams.Where(x => x.Name == "DropEvents").FirstOrDefault();
            var oldDropeventsUsers = Context.Users.Where(x => new string[] { "iwollmann", "apinto" }.Contains(x.UserName)).ToList();
            dropEvents.Members = new List<Member>();
            foreach (ApplicationUser user in oldDropeventsUsers) {
                dropEvents.Members.Add(new Member() {
                    TeamId = dropEvents.Id,
                    UserId = user.Id
                });
            }
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