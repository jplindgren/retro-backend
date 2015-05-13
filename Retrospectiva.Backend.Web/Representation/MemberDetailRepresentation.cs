using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Retrospectiva.Backend.Web.Representation {
    public class MemberDetailRepresentation : MemberRepresentation{
        public string TeamName { get; set; }
        public string Name { get; set; }
    }
}