using EmployeesEvaluation.Core.Models;

namespace EmployeesEvaluation.Repository.Repositories.Impl
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(EmployeesEvaluationContext context) : base(context) {}
    }
}