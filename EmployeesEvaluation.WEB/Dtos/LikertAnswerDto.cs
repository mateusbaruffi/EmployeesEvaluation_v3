using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesEvaluation.WEB.Dtos
{
    public class LikertAnswerDto
    {
        public int Id { get; set; }

        [Display(Name = "Answer")]
        public string Description { get; set; }
        //public Question Question { get; set; }
        public int QuestionId { get; set; }
    }
}
