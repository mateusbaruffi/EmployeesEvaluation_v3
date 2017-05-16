using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EmployeesEvaluation.Core.Models;
using System.Linq.Expressions;

namespace EmployeesEvaluation.Repository.Repositories
{
    public interface IUserRepository {

        ApplicationUser GetSingle(string id);
        IEnumerable<ApplicationUser> GetAll();
        IEnumerable<ApplicationUser> AllIncluding(params Expression<Func<ApplicationUser, object>>[] includeProperties);
        IEnumerable<ApplicationUser> FindBy(Expression<Func<ApplicationUser, bool>> predicate);
        IEnumerable<ApplicationUser> FindByIncluding(Expression<Func<ApplicationUser, bool>> predicate, params Expression<Func<ApplicationUser, object>>[] includeProperties);
        void Delete(ApplicationUser entity);
        void Commit();
    }
   
}