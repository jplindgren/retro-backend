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

    } //class
}