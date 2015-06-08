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
    [RoutePrefix("api/members")]
    [AllowCrossOrigin]
    public class MembersController : BaseApiController {
        [Route("")]
        public IEnumerable<MemberRepresentation> Get() {
            var representations = Context.Members.ToList().Select(x => ModelFactory.GetMemberRepresentation(x));
            return representations;
        }

        [Route("{userId:Guid}")]
        public MemberRepresentation Get(Guid userId) {
            return ModelFactory.GetMemberDetailRepresentation(GetMemberByUserId(userId));
        }

        // POST api/members
        [Route("")]
        [HttpPost]
        [AllowCrossOrigin]
        public HttpResponseMessage Post([FromBody]Member value){
            try{
                Context.Members.Add(value);
                Context.SaveChanges();
            }
            catch (Exception ex) {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
            var response = Request.CreateResponse(HttpStatusCode.Created, value);
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            return response;
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value) {
        }

        // DELETE api/values/5
        [Route("")]
        [HttpDelete]
        public void Delete(Guid id) {
            var member = GetMemberByUserId(id);
            Context.Members.Add(member);
            Context.SaveChanges();
        }

        #region Answers
        //[Route("{id:Guid}/sprint/{sprintId:Guid}/answer")]
        //[HttpPost]
        //public void AwnswerQuestion(Guid id, Guid sprintId, [FromBody]Answer answer) {
        //    var member = Context.Members.Include("SprintLookups").Where(x => x.Id == id).FirstOrDefault();
        //    member.AddAnswer(sprintId, answer);
        //    Context.SaveChanges();
        //}
        #endregion

        private Member GetMemberByUserId(Guid userId) {
            return Context.Members.Include("Team").Include("User").Where(x => x.UserId == userId.ToString()).FirstOrDefault();
        }
    } //class
}