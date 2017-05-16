using EmployeesEvaluation.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesEvaluation.Repository.Repositories
{
    public interface IEvaluationResponseRepository : IGenericRepository<EvaluationResponse> {

        EvaluationResponse GetSingleIncludingAll(Expression<Func<EvaluationResponse,bool>> predicate);

        IEnumerable<EvaluationResponse> FindByIncluding(Expression<Func<EvaluationResponse, bool>> predicate, params Expression<Func<EvaluationResponse, object>>[] includeProperties);

    }

}
