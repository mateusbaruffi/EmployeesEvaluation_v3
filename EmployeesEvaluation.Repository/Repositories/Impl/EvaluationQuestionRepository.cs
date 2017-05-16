using EmployeesEvaluation.Core.Models;

namespace EmployeesEvaluation.Repository.Repositories.Impl
{
    public class EvaluationQuestionRepository : GenericRepository<EvaluationQuestion>, IEvaluationQuestionRepository
    {
        public EvaluationQuestionRepository(EmployeesEvaluationContext context) : base(context) {}
    }
}