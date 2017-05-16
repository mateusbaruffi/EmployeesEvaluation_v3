using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeesEvaluation.Core.Models;
using EmployeesEvaluation.Repository.Repositories;
using System.Linq.Expressions;

namespace EmployeesEvaluation.Services.Impl
{
    public class UserRelationService : IUserRelationService
    {
       
        private IUserRelationRepository _userRelationRepository;

        public UserRelationService(IUserRelationRepository userRelationRepository)
        {
           
            this._userRelationRepository = userRelationRepository;
        }

        public IEnumerable<UserRelation> FindBy(Expression<Func<UserRelation, bool>> predicate)
        {
            return _userRelationRepository.FindBy(predicate);
        }

        public IEnumerable<UserRelation> FindByIncluding(Expression<Func<UserRelation, bool>> predicate, params Expression<Func<UserRelation, object>>[] includeProperties)
        {
            return _userRelationRepository.FindByIncluding(predicate, includeProperties);
        }

        public IEnumerable<ApplicationUser> GetEmployeesByDepartmentManagerId(string departmentManagerId)
        {
            ICollection<ApplicationUser> Employees = new List<ApplicationUser>();
            //IEnumerable<UserRelation> usersRelations = _userRelationRepository.GetEmployeesByDepartmentManagerId(departmentManagerId);
            IEnumerable<UserRelation> usersRelations = _userRelationRepository.FindByIncluding(ur => ur.DepartmentManagerId == departmentManagerId, ure => ure.Employee);

            foreach (var ur in usersRelations)
            {
                Employees.Add(ur.Employee);
            }

            return Employees.AsEnumerable();
        }

        public void DeleteWhere(Expression<Func<UserRelation, bool>> predicate)
        {
            _userRelationRepository.DeleteWhere(predicate);
            _userRelationRepository.Commit();
        }

        public void Create(UserRelation ur)
        {
            _userRelationRepository.Add(ur);
            _userRelationRepository.Commit();
        }

        public void AssignDepartmentManagerToEmployee(string employeeId, string[] departmentManagerIds)
        {
            if ((departmentManagerIds != null) && (departmentManagerIds.Count() > 0))
            {
                foreach (var dmId in departmentManagerIds)
                {

                    UserRelation ur = new UserRelation()
                    {
                        DepartmentManagerId = dmId,
                        EmployeeId = employeeId
                    };

                    _userRelationRepository.Add(ur);
                }

                _userRelationRepository.Commit();
            }
            
        }

    }
}
