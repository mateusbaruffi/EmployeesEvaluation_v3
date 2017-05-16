using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EmployeesEvaluation.Core.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {

        public UserType UserType { get; set; }

        public virtual ICollection<UserRelation> DepartmentManagersRelated { get; set; }
        public virtual ICollection<UserRelation> EmployeesRelated { get; set; }
        public virtual ICollection<EvaluationAssigned> EvaluationsAssigned { get; set; }
        public virtual ICollection<EvaluationResponse> EvaluationResponses { get; set; }

    }
}
