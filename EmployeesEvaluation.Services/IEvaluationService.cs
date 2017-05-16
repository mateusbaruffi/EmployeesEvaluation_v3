using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using EmployeesEvaluation.Core.Models;


namespace EmployeesEvaluation.Services
{

    public interface IEvaluationService : IGenericService<Evaluation>
    {
        IEnumerable<Evaluation> LoadAll();

        IEnumerable<EvaluationResponse> GetEvaluationResponses(string id);

        void CreateWithExistingQuestions(Evaluation evaluation, List<int> questionIds);

        void UpdateWithExistingQuestions(Evaluation evaluation, List<int> questionIds);

        void CreateEvaluationResponse(EvaluationResponse evaluationResponse);

        void AssignEvaluationEmployee(EvaluationAssigned evaluationAssigned);

        Evaluation GetEvaluationAssigned(int evaluationId, string employeeId);

        Evaluation GetSingleIncludingAll(Expression<Func<Evaluation, bool>> predicate);

        EvaluationResponse GetSingleResponseIncludingAll(int id);
    }

}