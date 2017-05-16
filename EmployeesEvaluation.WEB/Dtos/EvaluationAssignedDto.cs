using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesEvaluation.WEB.Dtos
{
    public class EvaluationAssignedDto
    {
        public int Id { get; set; }

        [Required]
        public int EvaluationId { get; set; }
        public string EmployeeId { get; set; }
        public string DepartmentManagerId { get; set; }
       
    }
}
