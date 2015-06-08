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

        public bool Current { get; set; }

        [ForeignKey("SprintId")]
        public Sprint Sprint { get; set; }
        public Guid SprintId { get; set; }

        [ForeignKey("TeamId")]
        public Team Team { get; set; }
        public Guid TeamId { get; set; }

        [InverseProperty("Retrospective")]
        public ICollection<RetrospectiveMember> Members { get; set; }

        [InverseProperty("Retrospective")]
        public ICollection<Question> Questions { get; set; }

        public void SetRetrospectiveMembers(List<Member> _members) {
            if (_members == null)
                return;
            if (Members == null)
                Members = new List<RetrospectiveMember>();

            _members.ForEach(x => Members.Add(new RetrospectiveMember() {
                UserId = x.UserId
            }));
        }
    } //class retrospective
}