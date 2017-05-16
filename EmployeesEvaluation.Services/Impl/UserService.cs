using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmployeesEvaluation.Repository.Repositories;
using EmployeesEvaluation.Core.Models;
using System.Linq.Expressions;

namespace EmployeesEvaluation.Services.Impl
{
    public class UserService : IUserService
    {

        private IUserRepository _userRepository;
        private IUserRelationRepository _userRelationRepository;

        public UserService(IUserRepository userRepository, IUserRelationRepository userRelationRepository)
        {
            this._userRepository = userRepository;
            this._userRelationRepository = userRelationRepository;
        }

        public IEnumerable<ApplicationUser> All()
        {
            return _userRepository.GetAll();
        }

        public IEnumerable<ApplicationUser> AllIncluding(params Expression<Func<ApplicationUser, object>>[] includeProperties)
        {
            return _userRepository.AllIncluding(includeProperties);
        }

        public void Add(UserRelation ur)
        {
            _userRelationRepository.Add(ur);
        }

        public virtual IEnumerable<ApplicationUser> FindBy(Expression<Func<ApplicationUser, bool>> predicate)
        {
            return _userRepository.FindBy(predicate);
        }

        public IEnumerable<ApplicationUser> FindByIncluding(Expression<Func<ApplicationUser, bool>> predicate, params Expression<Func<ApplicationUser, object>>[] includeProperties)
        {
            return _userRepository.FindByIncluding(predicate, includeProperties);
        }

        public IEnumerable<ApplicationUser> AllDepartmentManagers()
        {
            return _userRepository.FindBy(u => u.UserType == UserType.DM);
        }

        public IEnumerable<ApplicationUser> AllDepartmentManagersButMe(string userId)
        {
            return _userRepository.FindBy(u => u.UserType == UserType.DM && u.Id!=userId);
        }

        public void Delete(string id)
        {
            ApplicationUser user = _userRepository.GetSingle(id);

            _userRepository.Delete(user);
            _userRepository.Commit();

        }

    }
}
