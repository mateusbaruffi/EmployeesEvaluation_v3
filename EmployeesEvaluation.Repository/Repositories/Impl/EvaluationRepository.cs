using EmployeesEvaluation.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;

namespace EmployeesEvaluation.Repository.Repositories.Impl
{
    public class EvaluationRepository : GenericRepository<Evaluation>, IEvaluationRepository
    {

        private EmployeesEvaluationContext _context;
        public EvaluationRepository(EmployeesEvaluationContext context) : base(context) {

            this._context = context;
        }

        public IEnumerable<Evaluation> LoadAll()
        {
            IQueryable<Evaluation> query = this._context.Set<Evaluation>();
 
            //query = query.Include(a => a.EvaluationQuestions).ThenInclude(al => al.Question).ThenInclude(qt => qt.QuestionType);
            query = query.Include(a => a.EvaluationQuestions).ThenInclude(al => al.Question);

            return query.ToList();
        }

        public Evaluation GetSingleIncludingAll(Expression<Func<Evaluation, bool>> predicate)
        {
            IQueryable<Evaluation> query = _context.Set<Evaluation>();
            query = query.Include(e => e.EvaluationQuestions).ThenInclude(eq => eq.Question).ThenInclude(q => q.LikertAnswers);
            return query.Where(predicate).FirstOrDefault();
        }


    }
}