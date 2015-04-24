using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Retrospectiva.Backend.Web.Models {
    public class SprintLookup {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public bool Done { get; set; }

        [ForeignKey("SprintId")]
        public Sprint Sprint { get; set; }
        public Guid SprintId { get; set; }

        [ForeignKey("MemberId")]
        public Member Member { get; set; }
        public Guid MemberId { get; set; }
    }
}