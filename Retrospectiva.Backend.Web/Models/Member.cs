using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Retrospectiva.Backend.Web.Models {
    public class Member {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("TeamId")]
        public Team Team { get; set; }
        public Guid TeamId { get; set; }
        
        public ICollection<SprintLookup> SprintLookups { get; set; }
        public Guid SprintLookupId { get; set; }
    } //class
}