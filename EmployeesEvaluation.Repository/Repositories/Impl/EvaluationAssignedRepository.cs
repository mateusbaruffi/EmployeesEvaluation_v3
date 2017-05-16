using EmployeesEvaluation.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesEvaluation.Repository.Repositories.Impl
{
    public class EvaluationAssignedRepository : GenericRepository<EvaluationAssigned>, IEvaluationAssignedRepository
    {
        public EvaluationAssignedRepository(EmployeesEvaluationContext context) : base(context) { }
    }

}
