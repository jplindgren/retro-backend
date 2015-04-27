using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Retrospectiva.Backend.Web.Models {
    public class Answer {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Text { get; set; }

        [ForeignKey("QuestionId")]
        public Question Question { get; set; }
        public Guid QuestionId { get; set; }

        [ForeignKey("MemberId")]
        public Member Member { get; set; }
        public Guid MemberId { get; set; }

        [ForeignKey("RetrospectiveId")]
        public SprintRetrospective Retrospective { get; set; }
        public Guid RetrospectiveId { get; set; }
    } //class
}