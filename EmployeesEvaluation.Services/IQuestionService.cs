using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using EmployeesEvaluation.Core.Models;


namespace EmployeesEvaluation.Services
{

    public interface IQuestionService : IGenericService<Question>
    {
        IEnumerable<Question> FindBy(Expression<Func<Question, bool>> predicate);
    }

}