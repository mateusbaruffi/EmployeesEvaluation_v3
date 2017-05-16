using EmployeesEvaluation.Core.Models;

namespace EmployeesEvaluation.Repository.Repositories.Impl
{
    public class SeasonRepository : GenericRepository<Season>, ISeasonRepository
    {
        public SeasonRepository(EmployeesEvaluationContext context) : base(context) {}
    }
}