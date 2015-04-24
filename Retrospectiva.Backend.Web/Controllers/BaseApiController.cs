using Retrospectiva.Backend.Web.Repository;
using Retrospectiva.Backend.Web.Representation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Retrospectiva.Backend.Web.Controllers {
    public class BaseApiController : ApiController {
        protected RetroContext Context;
        protected ModelFactory ModelFactory;
        public BaseApiController() {
            Context = new RetroContext();
            ModelFactory = new ModelFactory();
        }
    } //class
}