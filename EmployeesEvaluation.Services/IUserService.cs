using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeesEvaluation.Core.Models;
using System.Linq.Expressions;

namespace EmployeesEvaluation.Services
{
    public interface IUserService
    {

        IEnumerable<ApplicationUser> All();
        void Add(UserRelation ur);
        IEnumerable<ApplicationUser> FindBy(Expression<Func<ApplicationUser, bool>> predicate);
        IEnumerable<ApplicationUser> FindByIncluding(Expression<Func<ApplicationUser, bool>> predicate, params Expression<Func<ApplicationUser, object>>[] includeProperties);
        IEnumerable<ApplicationUser> AllIncluding(params Expression<Func<ApplicationUser, object>>[] includeProperties);
        IEnumerable<ApplicationUser> AllDepartmentManagers();
        IEnumerable<ApplicationUser> AllDepartmentManagersButMe(string userId);
        void Delete(string id);

    }
}
