using System;
using System.Collections.Generic;

namespace EmployeesEvaluation.Core.Models
{
    public class Question : EntityBase
    {
        public string Description { get; set; }
        public int Limit { get; set; }
        public QuestionType QuestionType { get; set; }
        public ApplicationUser Ownership { get; set; }
        public string OwnershipId { get; set; }
        public ICollection<EvaluationQuestion> EvaluationQuestions { get; set; }
        public ICollection<LikertAnswer> LikertAnswers { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}