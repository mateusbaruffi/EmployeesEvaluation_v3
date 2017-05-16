using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesEvaluation.Core.Models
{
    public class EvaluationAssigned : EntityBase
    {
        public string DepartmentManagerId { get; set; }

        public Evaluation Evaluation { get; set; }
        public int EvaluationId { get; set; }

        public ApplicationUser Employee { get; set; }
        public string EmployeeId { get; set; }
    }
}
