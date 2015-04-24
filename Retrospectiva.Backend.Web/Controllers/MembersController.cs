using Retrospectiva.Backend.Web.Models;
using Retrospectiva.Backend.Web.Representation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Retrospectiva.Backend.Web.Controllers {
    public class MembersController : BaseApiController {
        // GET api/values
        public IEnumerable<Member> Get() {
            var test = Context.Members.ToList();
            return test;
        }

        // GET api/values/5
        public MemberRepresentation Get(Guid id) {
            return ModelFactory.GetMemberRepresentation(GetMember(id));
        }

        // POST api/values
        public void Post([FromBody]Member value) {
            var team = new Team(){
                Name = "Team DropEvents"
            };
            Context.Teams.Add(team);
            Context.SaveChanges();

            value = new Member() {
                Name = "Joao Lindgren",
                Team = team
            };
            Context.Members.Add(value);
            Context.SaveChanges();
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value) {
        }

        // DELETE api/values/5
        public void Delete(Guid id) {
            var member = GetMember(id);
            Context.Members.Add(member);
            Context.SaveChanges();
        }

        private Member GetMember(Guid id) {
            return Context.Members.Where(x => x.Id == id).FirstOrDefault();            
        }
    } //class
}