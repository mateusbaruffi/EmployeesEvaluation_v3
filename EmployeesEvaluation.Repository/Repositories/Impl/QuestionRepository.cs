using EmployeesEvaluation.Core.Models;

namespace EmployeesEvaluation.Repository.Repositories.Impl
{
    public class QuestionRepository : GenericRepository<Question>, IQuestionRepository
    {
        public QuestionRepository(EmployeesEvaluationContext context) : base(context) {}
    }
}