﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Retrospectiva.Backend.Web.Models {
    public class Question {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Description { get; set; }

        [ForeignKey("RetrospectiveId")]
        public SprintRetrospective Retrospective { get; set; }
        public Guid RetrospectiveId { get; set; }

        public ICollection<Answer> Answers { get; set; }
    } //class
}