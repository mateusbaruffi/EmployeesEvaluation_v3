using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace EmployeesEvaluation.WEB.Dtos
{
    public class QuestionAnswerDto
    {
        public int QuestionId { get; set; }
        public int LikertAnswerId { get; set; }
        public string OpenEndedAnswer { get; set; }
        public string FileName { get; set; }
        public IFormFile File { get; set; }
        public QuestionDto Question { get; set; }
    }
}
