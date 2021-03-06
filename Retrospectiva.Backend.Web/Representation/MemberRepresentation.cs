﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Retrospectiva.Backend.Web.Representation {
    public class MemberRepresentation {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public Guid TeamId { get; set; }
        public Guid SprintId { get; set; }
    }
}