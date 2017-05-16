using EmployeesEvaluation.Core.Models;

namespace EmployeesEvaluation.Repository.Repositories.Impl
{
    public class LikertAnswerRepository : GenericRepository<LikertAnswer>, ILikertAnswerRepository
    {
        public LikertAnswerRepository(EmployeesEvaluationContext context) : base(context) { }
    }
}