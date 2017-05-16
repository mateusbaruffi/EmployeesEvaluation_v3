using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeesEvaluation.Core.Models;
using System.Linq.Expressions;

namespace EmployeesEvaluation.Services
{
    public interface IUserRelationService
    {

        IEnumerable<UserRelation> FindBy(Expression<Func<UserRelation, bool>> predicate);
        IEnumerable<UserRelation> FindByIncluding(Expression<Func<UserRelation, bool>> predicate, params Expression<Func<UserRelation, object>>[] includeProperties);
        void DeleteWhere(Expression<Func<UserRelation, bool>> predicate);
        void Create(UserRelation ur);
        void AssignDepartmentManagerToEmployee(string employeeId, string[] departmentManagerIds);
        IEnumerable<ApplicationUser> GetEmployeesByDepartmentManagerId(string departmentManagerId);

    }
}
