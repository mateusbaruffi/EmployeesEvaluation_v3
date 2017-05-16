using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmployeesEvaluation.Core.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EmployeesEvaluation.Repository.Repositories.Impl
{
    public class UserRepository : IUserRepository
    {
        private EmployeesEvaluationContext _context;

        public UserRepository(EmployeesEvaluationContext context) {
            this._context = context;
        }

        public virtual ApplicationUser GetSingle(string id)
        {
            return _context.Set<ApplicationUser>().FirstOrDefault(x => x.Id == id);
        }

        public virtual IEnumerable<ApplicationUser> GetAll()
        {
            return _context.Set<ApplicationUser>().AsEnumerable();
        }

        public virtual IEnumerable<ApplicationUser> AllIncluding(params Expression<Func<ApplicationUser, object>>[] includeProperties)
        {
            IQueryable<ApplicationUser> query = _context.Set<ApplicationUser>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.AsEnumerable();
        }

        public virtual IEnumerable<ApplicationUser> FindBy(Expression<Func<ApplicationUser, bool>> predicate)
        {
            return _context.Set<ApplicationUser>().Where(predicate);
        }

        public virtual IEnumerable<ApplicationUser> FindByIncluding(Expression<Func<ApplicationUser, bool>> predicate, params Expression<Func<ApplicationUser, object>>[] includeProperties)
        {
            IQueryable<ApplicationUser> query = _context.Set<ApplicationUser>();
            /*foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }*/

            query.Include(u => u.EmployeesRelated).Where(predicate);

            return query.Where(predicate);
        }

        public virtual void Delete(ApplicationUser entity)
        {
            EntityEntry dbEntityEntry = _context.Entry<ApplicationUser>(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        public virtual void Commit()
        {
            _context.SaveChanges();
        }
    }
}