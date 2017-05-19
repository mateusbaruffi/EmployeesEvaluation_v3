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
using EmployeesEvaluation.WEB.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace EmployeesEvaluation.WEB.Controllers
{
    [Authorize(Roles = "HRM, DM, EMP")]
    public class EvaluationsController : Controller
    {
        private readonly ILogger _logger;
        private readonly IEvaluationService _evaluationService;
        private readonly IUserService _userService;
        private readonly ISeasonService _seasonService;
        private readonly IEmailSender _emailSender;
        private readonly IHostingEnvironment _environment;

        public EvaluationsController(IHostingEnvironment hostingEnviroment, IEmailSender emailSender, ILogger<EvaluationsController> logger, ISeasonService seasonService, IEvaluationService evaluationService, IUserService userService)
        {
            this._logger = logger;
            this._evaluationService = evaluationService;
            this._userService = userService;
            this._seasonService = seasonService;
            this._emailSender = emailSender;
            this._environment = hostingEnviroment;
        }

        [Authorize(Roles = "HRM, DM")]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NewPolymer()
        {
            return View();
        }

        [Authorize(Roles = "HRM, DM")]
        public IActionResult New()
        {
            EvaluationDto evaluationDto = new EvaluationDto();

            // get all Department Managers from users
            evaluationDto.DepartmentManagers = _userService.FindBy(u => u.UserType == UserType.DM).Select(u => new SelectListItem
            {
                Text = u.Email,
                Value = u.Id
            }).ToList();

            // get all seasons
            evaluationDto.Seasons = _seasonService.All().Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.Id.ToString()
            }).ToList();

            return View("Form", evaluationDto);
        }

        [Authorize(Roles = "HRM, DM")]
        public IActionResult Edit(int id)
        {
            var evaluation = _evaluationService.GetSingleIncludingAll(e => e.Id == id);

            var evaluationDto = Mapper.Map<Evaluation, EvaluationDto>(evaluation);

            // get all Department Managers from users
            evaluationDto.DepartmentManagers = _userService.FindBy(u => u.UserType == UserType.DM).Select(u => new SelectListItem
            {
                Text = u.Email,
                Value = u.Id
            }).ToList();

            // get all seasons
            evaluationDto.Seasons = _seasonService.All().Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.Id.ToString()
            }).ToList();

            return View("Form", evaluationDto);
        }

        [Authorize(Roles = "HRM, DM")]
        public IActionResult Save(EvaluationDto evaluationDto)
        {
            var evaluation = Mapper.Map<EvaluationDto, Evaluation>(evaluationDto);

            if (evaluation.Id == 0)
            {
                _logger.LogInformation("--------------- Creating evaluation -----------------");
                _evaluationService.CreateWithExistingQuestions(evaluation, evaluationDto.QuestionIds);
            }
            else
            {
                _logger.LogInformation("--------------- UPDATING evaluation -----------------");
                _evaluationService.UpdateWithExistingQuestions(evaluation, evaluationDto.QuestionIds);
            }

            TempData["messageType"] = "toast-success";
            TempData["messageText"] = "Evaluation has been successfully saved!";

            return RedirectToAction("Index", "Evaluations");
        }

        [Authorize(Roles = "HRM, DM")]
        public IActionResult Report()
        {
            return View();
        }


        [Authorize(Roles = "HRM, DM")]
        public IActionResult Assign()
        {
            return View();
        }

        [Authorize(Roles = "HRM, DM")]
        public async Task<IActionResult> AssignSave(EvaluationAssignedDto evaluationAssignedDto)
        {
            var evaluationAssigned = Mapper.Map<EvaluationAssignedDto, EvaluationAssigned>(evaluationAssignedDto);

            try
            {
                _evaluationService.AssignEvaluationEmployee(evaluationAssigned);
                await SendEmployeeEmail(evaluationAssignedDto);
            }
            catch (Exception e)
            {
                _logger.LogError("-------------- Something went wrong -----------", e);
            }

            TempData["messageType"] = "toast-success";
            TempData["messageText"] = "Evaluation has been successfully assigned!";
            return RedirectToAction("Index", "Evaluations");
        }


        [Authorize(Roles = "HRM, DM, EMP")]
        public IActionResult Responses()
        {
            return View();
        }


        [Authorize(Roles = "HRM, DM, EMP")]
        public IActionResult ShowResponse(int id)
        {
            EvaluationResponse evaluationResponse = _evaluationService.GetSingleResponseIncludingAll(id);

            var evaluationResponseDto = Mapper.Map<EvaluationResponse, EvaluationResponseDto>(evaluationResponse);

            return View(evaluationResponseDto);
        }


        [Authorize(Roles = "HRM, DM, EMP")]
        public IActionResult Reply(int id, string employeeId, string departmentManagerId)
        {
            var evaluation = _evaluationService.GetEvaluationAssigned(id, employeeId);
            var evaluationDto = Mapper.Map<Evaluation, EvaluationDto>(evaluation);

            var evaluationResopnseDto = new EvaluationResponseDto();
            evaluationResopnseDto.Evaluation = evaluationDto;
            evaluationResopnseDto.EvaluationId = id;
            evaluationResopnseDto.EmployeeId = employeeId;
            evaluationResopnseDto.DepartmentManagerId = departmentManagerId;

            if (evaluation == null)
                _logger.LogInformation("--------------- This Employee Has No Evaluation To Answer ");
            else
            {
                // verify if this evaluation has already been answered
            }

            return View(evaluationResopnseDto);
        }

        [Authorize(Roles = "HRM, DM, EMP")]
        public async Task<IActionResult> SaveReply(EvaluationResponseDto evaluationResponseDto, IFormFile File)
        {
            _logger.LogInformation("---------------- " + evaluationResponseDto );

            await UploadAnswersFile(evaluationResponseDto.QuestionAnswers);

            var evaluationResponse = Mapper.Map<EvaluationResponseDto, EvaluationResponse>(evaluationResponseDto);
            _evaluationService.CreateEvaluationResponse(evaluationResponse);

            await SendDepartmentManagerEmail(evaluationResponseDto);


            TempData["messageType"] = "toast-success";
            TempData["messageText"] = "Evaluation has been successfully answered!";

            return RedirectToAction("Responses", "Evaluations");
        }

        public IActionResult Success()
        {
            return View();
        }

        private async Task UploadAnswersFile(ICollection<QuestionAnswerDto> questionAnswers)
        {
            var uploads = Path.Combine(_environment.WebRootPath, "uploads");

            foreach (var answer in questionAnswers)
            {
                if ((answer.File != null) && (answer.File.Length > 0))
                {
                    _logger.LogInformation("--------------------- Starting File Upload");

                    using (var fileStream = new FileStream(Path.Combine(uploads, answer.File.FileName), FileMode.Create))
                    {
                        await answer.File.CopyToAsync(fileStream);
                    }
                }
            }
        }

        private async Task SendEmployeeEmail(EvaluationAssignedDto evaluationAssignedDto)
        {
            // get the employee's email
            var employee = _userService.FindBy(u => u.Id == evaluationAssignedDto.EmployeeId).FirstOrDefault();
            var evaluationId = evaluationAssignedDto.EvaluationId;

            var link = $"http://localhost:63585/Evaluations/Reply/{evaluationId}?employeeId={employee.Id}&departmentManagerId={evaluationAssignedDto.DepartmentManagerId}";
           
            await _emailSender.SendEmailAsync(employee.Email, "Employees Evaluation",
                $"Hi {employee.Email}, <br /> we would like you to take a time to fill up our evaluation.<br /><br /> <a href='{link}' target='_blank'>Fill Up the Evaluation</a>");
            
        }

        private async Task SendDepartmentManagerEmail(EvaluationResponseDto evaluationResponseDto)
        {
            // get the DM email
            var departmentManager = _userService.FindBy(u => u.Id == evaluationResponseDto.DepartmentManagerId).FirstOrDefault();
            var evaluationId = evaluationResponseDto.EvaluationId;

            var link = $"http://localhost:63585/Evaluations/ShowResponse/{evaluationId}?employeeId={departmentManager.Id}";

            await _emailSender.SendEmailAsync(departmentManager.Email, "Employees Evaluation",
                $"Hi {departmentManager.Email}, <br /> A new evaluation has been answered.<br /><br /> <a href='{link}' target='_blank'>Check it now!</a>");

        }


    }
}