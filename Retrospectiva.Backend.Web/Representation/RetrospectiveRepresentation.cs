using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Retrospectiva.Backend.Web.Representation {
    public class RetrospectiveRepresentation {
        public Guid Id { get; set; }
        public int SprintNumber { get; set; }
        public string TeamName { get; set; }

        public Guid SprintId { get; set; }
        public Guid TeamId { get; set; }
    }
}