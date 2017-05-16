using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using EmployeesEvaluation.Core.Models;
using EmployeesEvaluation.Repository.Repositories;

namespace EmployeesEvaluation.Services.Impl
{
    public class SeasonService : ISeasonService
    {
        private ISeasonRepository _seasonRepository;
 
        public SeasonService(ISeasonRepository seasonRepository) 
        { 
            this._seasonRepository = seasonRepository; 
        } 

        public IEnumerable<Season> AllIncluding(params Expression<Func<Season, object>>[] includeProperties)
        {
            IEnumerable<Season> _seasons = _seasonRepository
                .AllIncluding(includeProperties)
                .ToList();

            return _seasons;
        }

        public IEnumerable<Season> All() 
        { 
            return _seasonRepository.GetAll(); 
        } 
 
        public Season Get(int id) 
        { 
            return _seasonRepository.GetSingle(id); 
        }

        public Season GetSingleIncluding(Expression<Func<Season, bool>> predicate, params Expression<Func<Season, object>>[] includeProperties)
        {
            return _seasonRepository.GetSingleIncluding(predicate, includeProperties);
        }

        public void Create(Season season) 
        { 
            _seasonRepository.Add(season); 
            _seasonRepository.Commit();
        } 
        
        public void Update(Season season) 
        { 
            _seasonRepository.Update(season); 
            _seasonRepository.Commit();
        } 
 
        public void Delete(int id) 
        {             
            Season season = Get(id); 
            _seasonRepository.Delete(season); 
            _seasonRepository.Commit(); 
        } 
    }
}