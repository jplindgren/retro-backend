using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Retrospectiva.Backend.Web.Models {
    public class SprintRetrospective {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [ForeignKey("SprintId")]
        public Sprint Sprint { get; set; }
        public Guid SprintId { get; set; }

        [ForeignKey("TeamId")]
        public Team Team { get; set; }
        public Guid TeamId { get; set; }

        public ICollection<Member> Members { get; set; }
        public ICollection<Answer> Answers { get; set; }

        [InverseProperty("Retrospective")]
        public ICollection<Question> Questions { get; set; }
    }
}