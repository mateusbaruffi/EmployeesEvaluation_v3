using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using EmployeesEvaluation.Core.Models;
using EmployeesEvaluation.Repository.Repositories;
using Microsoft.Extensions.Logging;

namespace EmployeesEvaluation.Services.Impl
{
    public class QuestionService : IQuestionService
    {
        private IQuestionRepository _questionRepository;
        private ILikertAnswerRepository _likertAnswerRepository;

        private readonly ILogger _logger;
 
        public QuestionService(IQuestionRepository questionRepository, ILikertAnswerRepository likertAnswerRepository, ILogger<QuestionService> logger) 
        { 
            this._questionRepository = questionRepository;
            this._likertAnswerRepository = likertAnswerRepository;
            this._logger = logger;
        } 

        public IEnumerable<Question> AllIncluding(params Expression<Func<Question, object>>[] includeProperties)
        {
            IEnumerable<Question> _questions = _questionRepository
                .AllIncluding(includeProperties)
                .ToList();

            return _questions;
        }

        public IEnumerable<Question> All() 
        { 
            return _questionRepository.GetAll(); 
        } 
 
        public Question Get(int id) 
        { 
            return _questionRepository.GetSingle(id); 
        } 

        public Question GetSingleIncluding(Expression<Func<Question, bool>> predicate, params Expression<Func<Question, object>>[] includeProperties)
        {
            return _questionRepository.GetSingleIncluding(predicate, includeProperties);
        }

        public virtual IEnumerable<Question> FindBy(Expression<Func<Question, bool>> predicate)
        {
            return _questionRepository.FindBy(predicate);
        }

        public void Create(Question question) 
        {
            _questionRepository.Add(question); 
            _questionRepository.Commit();
        } 
        
        public void Update(Question question) 
        {
            // Update question attributes
            _questionRepository.Update(question); 

            // update the answers if QuestionType is equal LikertScale
            if (question.QuestionType == QuestionType.LikertScale)
            {
                // insert the new ones
                foreach (var likertAnswer in question.LikertAnswers.ToList())
                {
                    LikertAnswer la = new LikertAnswer { QuestionId = question.Id, Description = likertAnswer.Description };
                    _likertAnswerRepository.Add(la);
                }

                // remove all current answers
                _likertAnswerRepository.DeleteWhere(l => l.QuestionId == question.Id);
                
            }
            _questionRepository.Commit();
        } 
 
        public void Delete(int id) 
        {             
            Question question = Get(id); 
            _questionRepository.Delete(question); 
            _questionRepository.Commit(); 
        } 
    }
}