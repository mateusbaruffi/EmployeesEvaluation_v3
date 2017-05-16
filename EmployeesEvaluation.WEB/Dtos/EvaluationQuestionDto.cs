using System;

namespace EmployeesEvaluation.WEB.Dtos
{
    public class EvaluationQuestionDto
    {
        public int Id { get; set; }
        public int EvaluationId { get; set; }
        public EvaluationDto Evaluation { get; set; }

        public int QuestionId { get; set; }
        public QuestionDto Question { get; set; }
    }
}