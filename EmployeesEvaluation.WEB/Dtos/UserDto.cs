using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesEvaluation.WEB.Dtos
{
    public class UserDto
    {
        public string Id { get; set; }
        public string DepartmentManagerId { get; set; }
        public string EmployeeId { get; set; }
        public string Email { get; set; }

    }
}
