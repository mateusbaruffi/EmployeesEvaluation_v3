using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesEvaluation.Core.Models
{
    public class LikertAnswer : EntityBase
    {
        public string Description { get; set; }
        public Question Question { get; set; }
        public int QuestionId { get; set; }
    }
}
