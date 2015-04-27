using Retrospectiva.Backend.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Retrospectiva.Backend.Web.Representation {
    public class ModelFactory {
        public MemberRepresentation GetMemberRepresentation(Member member) {
            return new MemberRepresentation() {
                Id = member.Id,
                Name = member.Name,
                TeamId = member.TeamId
            };
        }

        public MemberDetailRepresentation GetMemberDetailRepresentation(Member member) {
            return new MemberDetailRepresentation() {
                Id = member.Id,
                Name = member.Name,
                TeamId = member.TeamId,
                TeamName = member.Team.Name
            };
        }

        public RetrospectiveRepresentation GetRetrospectiveRepresentation(SprintRetrospective retrospective) {
            return new RetrospectiveRepresentation() {
                Id = retrospective.Id,
                SprintNumber = retrospective.Sprint.Number,
                TeamName = retrospective.Team.Name,
                SprintId = retrospective.SprintId,
                TeamId = retrospective.TeamId
            };
        }

        public RetrospectiveDetailRepresentation GetRetrospectiveDetailRepresentation(SprintRetrospective retrospective) {
            return new RetrospectiveDetailRepresentation() {
                Id = retrospective.Id,
                SprintNumber = retrospective.Sprint.Number,
                TeamName = retrospective.Team.Name,
                SprintId = retrospective.SprintId,
                TeamId = retrospective.TeamId,
                Questions = retrospective.Questions.Select(x => GetQuestionRepresentation(x)),
                Members = retrospective.Members.Select(x => GetMemberRepresentation(x))
            };
        }

        public SprintRepresentation GetSprintRepresentation(Sprint sprint) {
            return new SprintRepresentation() {
                Id = sprint.Id,
                SprintNumber = sprint.Number
            };
        }

        public QuestionRepresentation GetQuestionRepresentation(Question question) { 
            return new QuestionRepresentation() {
                Id = question.Id,
                Description = question.Description
            };
        }
    } //class
}