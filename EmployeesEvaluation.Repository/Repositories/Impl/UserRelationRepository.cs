using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using EmployeesEvaluation.Core.Models;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace EmployeesEvaluation.Repository.Repositories.Impl
{
    public class UserRelationRepository : IUserRelationRepository
    {
        private EmployeesEvaluationContext _context;

        public UserRelationRepository(EmployeesEvaluationContext context)
        {
            this._context = context;
        }

        public virtual IEnumerable<UserRelation> FindBy(Expression<Func<UserRelation, bool>> predicate)
        {
            return _context.Set<UserRelation>().Where(predicate);
        }

        public virtual IEnumerable<UserRelation> FindByIncluding(Expression<Func<UserRelation, bool>> predicate, params Expression<Func<UserRelation, object>>[] includeProperties)
        {
            IQueryable<UserRelation> query = _context.Set<UserRelation>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.Where(predicate);
        }

        public virtual void Add(UserRelation ur)
        {
            EntityEntry dbEntityEntry = _context.Entry<UserRelation>(ur);
            _context.Set<UserRelation>().Add(ur);
        }

        public virtual void DeleteWhere(Expression<Func<UserRelation, bool>> predicate)
        {
            IEnumerable<UserRelation> entities = _context.Set<UserRelation>().Where(predicate);

            foreach (var entity in entities)
            {
                _context.Entry<UserRelation>(entity).State = EntityState.Deleted;
            }
        }

        public virtual void Commit()
        {
            _context.SaveChanges();
        }
    }
}
