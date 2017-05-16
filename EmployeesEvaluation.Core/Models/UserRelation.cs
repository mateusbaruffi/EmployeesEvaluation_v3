using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesEvaluation.Core.Models
{
    public class UserRelation
    {
        public string EmployeeId { get; set; }
        public string DepartmentManagerId { get; set; }

        public virtual ApplicationUser Employee { get; set; }
        public virtual ApplicationUser DepartmentManager { get; set; }
    }
}
