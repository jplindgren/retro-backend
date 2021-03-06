﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Retrospectiva.Backend.Web.Representation {
    public class QuestionRepresentation {
        public Guid Id { get; set; }
        public string Description { get; set; }
    } //class

    public class CurrentRetrospectiveQuestionsRepresentation {
        public int Sprint { get; set; }
        public QuestionRepresentation[] Questions { get; set; }
    } //class
}