using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using EmployeesEvaluation.Core.Models;
using EmployeesEvaluation.Repository.Repositories;

namespace EmployeesEvaluation.Services.Impl
{
    public class DepartmentService : IDepartmentService
    {
        private IDepartmentRepository _departmentRepository;
 
        public DepartmentService(IDepartmentRepository departmentRepository) 
        { 
            this._departmentRepository = departmentRepository; 
        } 

        public IEnumerable<Department> AllIncluding(params Expression<Func<Department, object>>[] includeProperties)
        {
            IEnumerable<Department> _departments = _departmentRepository
                .AllIncluding(includeProperties)
                .ToList();

            return _departments;
        }

        public IEnumerable<Department> All() 
        { 
            return _departmentRepository.GetAll(); 
        } 
 
        public Department Get(int id) 
        { 
            return _departmentRepository.GetSingle(id); 
        }

        public Department GetSingleIncluding(Expression<Func<Department, bool>> predicate, params Expression<Func<Department, object>>[] includeProperties)
        {
            return _departmentRepository.GetSingleIncluding(predicate, includeProperties);
        }

        public void Create(Department department) 
        { 
            _departmentRepository.Add(department); 
            _departmentRepository.Commit();
        } 
        
        public void Update(Department department) 
        { 
            _departmentRepository.Update(department); 
            _departmentRepository.Commit();
        } 
 
        public void Delete(int id) 
        {             
            Department department = Get(id); 
            _departmentRepository.Delete(department); 
            _departmentRepository.Commit(); 
        } 
    }
}