using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesEvaluation.WEB.Dtos
{
    public class EvaluationResponseDto
    {
        public int Id { get; set; }
        public int EvaluationId { get; set; }
        public EvaluationDto Evaluation { get; set; }
        public string DepartmentManagerId { get; set; }
        public string EmployeeId { get; set; }
        public List<QuestionAnswerDto> QuestionAnswers { get; set; }
        public UserDto Employee { get; set; }
        

    }
}
