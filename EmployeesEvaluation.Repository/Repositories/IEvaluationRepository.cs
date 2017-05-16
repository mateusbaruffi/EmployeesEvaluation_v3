using EmployeesEvaluation.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EmployeesEvaluation.Repository.Repositories
{
    public interface IEvaluationRepository : IGenericRepository<Evaluation> { 
        IEnumerable<Evaluation> LoadAll();

        Evaluation GetSingleIncludingAll(Expression<Func<Evaluation, bool>> predicate);
    }
}