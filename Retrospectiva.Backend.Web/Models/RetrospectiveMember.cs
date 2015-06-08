using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Retrospectiva.Backend.Web.Models {
    public class RetrospectiveMember {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey("RetrospectiveId")]
        public SprintRetrospective Retrospective { get; set; }
        public Guid RetrospectiveId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }

        public ICollection<Answer> Answers { get; set; }

        internal void AddAnswers(List<Answer> answers) {
            if (Answers == null)
                Answers = new List<Answer>();

            answers.ForEach(x => Answers.Add(x));
        }
    } //class
}