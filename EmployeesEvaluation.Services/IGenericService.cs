using System;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace EmployeesEvaluation.Services
{

    public interface IGenericService<T>
    {
        IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        IEnumerable<T> All(); 
        T Get(int id);
        T GetSingleIncluding(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties);
        void Create(T t); 
        void Update(T t); 
        void Delete(int id);
    }

}