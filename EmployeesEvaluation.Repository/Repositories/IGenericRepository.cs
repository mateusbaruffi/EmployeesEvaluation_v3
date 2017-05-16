using System;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace EmployeesEvaluation.Repository.Repositories
{
    public interface IGenericRepository<T> where T : class, new()
    {
        IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> GetAll();
        int Count();
        T GetSingle(int id);
        T GetSingleIncluding(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteWhere(Expression<Func<T, bool>> predicate);
        void Commit();
        
    }
}