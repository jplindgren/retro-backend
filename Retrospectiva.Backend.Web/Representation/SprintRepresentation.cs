using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Retrospectiva.Backend.Web.Representation {
    public class SprintRepresentation {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public IList<QuestionRepresentation> Questions { get; set; }
    }
}