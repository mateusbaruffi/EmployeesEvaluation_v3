using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmployeesEvaluation.WEB.Dtos;
using Microsoft.Extensions.Logging;
using AutoMapper;
using EmployeesEvaluation.Core.Models;
using EmployeesEvaluation.Services;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace EmployeesEvaluation.WEB.Controllers
{
    [Authorize(Roles = "HRM, DM")]
    public class QuestionsController : Controller
    {
        private readonly ILogger _logger;
        private readonly IQuestionService _questionService;
        private readonly IUserService _userService;

        public QuestionsController(ILogger<QuestionsController> logger, IQuestionService questionService, IUserService userService)
        {
            this._logger = logger;
            this._questionService = questionService;
            this._userService = userService;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NewPolymer()
        {
            return View();
        }

        public IActionResult New()
        {
            QuestionDto questionDto = new QuestionDto();
            questionDto.QuestionType = new QuestionTypeDto();

            // get all HRM and DM from users
            questionDto.HR_DM_Managers = _userService.FindBy(u => u.UserType == UserType.HRM || u.UserType == UserType.DM).Select(u => new SelectListItem
            {
                Text = u.Email,
                Value = u.Id
            }).ToList();

            return View("Form", questionDto);
        }

        public IActionResult Edit(int id)
        {
            var question = _questionService.GetSingleIncluding(q => q.Id == id, q => q.LikertAnswers);

            var questionDto = Mapper.Map<Question, QuestionDto>(question);

            // get all HRM and DM from users
            questionDto.HR_DM_Managers = _userService.FindBy(u => u.UserType == UserType.HRM || u.UserType == UserType.DM).Select(u => new SelectListItem
            {
                Text = u.Email,
                Value = u.Id
            }).ToList();

            return View("Form", questionDto);
        }

        public IActionResult Save(QuestionDto questionDto)
        {
            var question = Mapper.Map<QuestionDto, Question>(questionDto);


            if (question.Id == 0)
            {
                _logger.LogInformation("--------------- Creating question -----------------");
                _questionService.Create(question);
            }
            else
            {
                _logger.LogInformation("--------------- UPDATING question -----------------");
                _questionService.Update(question);
            }


            TempData["message"] = "toast-success";

            return RedirectToAction("Index", "Questions");
        }
    }
}