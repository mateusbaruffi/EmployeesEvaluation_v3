using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using EmployeesEvaluation.WEB.Dtos;
using EmployeesEvaluation.Core.Models;
using EmployeesEvaluation.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Microsoft.AspNetCore.Identity;

namespace EmployeesEvaluation.WEB.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class EvaluationsController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEvaluationService _evaluationService;
        private readonly ILogger _logger;

        public EvaluationsController(IEvaluationService evaluationService, ILogger<EvaluationsController> logger, UserManager<ApplicationUser> userManager)
        {
            this._evaluationService = evaluationService;
            this._logger = logger;
            this._userManager = userManager;
        }

        [HttpPost("List")]
        public JsonResult List([DataSourceRequest]DataSourceRequest request)
        {
            var result = _evaluationService.All().Select(Mapper.Map<Evaluation, EvaluationDto>);
            var dsResult = result.ToDataSourceResult(request);
            return Json(dsResult);
        }

        [HttpGet("All")]
        public IActionResult All([DataSourceRequest]DataSourceRequest request, string text)
        {
            var result = _evaluationService.All().Select(Mapper.Map<Evaluation, EvaluationDto>);
            //var result = _questionService.All().Select(Mapper.Map<Question, QuestionDto>);
            //var dsResult = result.ToDataSourceResult(request);
            return Json(result.ToList());
        }

        [HttpPost("Delete")]
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, EvaluationDto evaluationDto)
        {
            //var evaluation = Mapper.Map<EvaluationDto, Evaluation>(evaluationDto);

            _evaluationService.Delete(evaluationDto.Id);

            return Json(new { Data = evaluationDto });
        }

        [HttpPost("ListResponses")]
        public JsonResult ListResponses([DataSourceRequest]DataSourceRequest request)
        {
            // emp only see their data 
            // dm see their data and their emps
            // hr all
            //User.
            var userId = _userManager.GetUserId(HttpContext.User);

            _logger.LogInformation("----------------userId: " + userId);

            var result = _evaluationService.GetEvaluationResponses(userId).Select(Mapper.Map<EvaluationResponse, EvaluationResponseDto>);
            var dsResult = result.ToDataSourceResult(request);
            return Json(dsResult);
        }

        [HttpPost("Create")]
        [AllowAnonymous]
        public IActionResult Create([FromBody]EvaluationDto evaluationDto)
        {
            if(!ModelState.IsValid)
                return BadRequest();

            var evaluation = Mapper.Map<EvaluationDto, Evaluation>(evaluationDto);
            
            _evaluationService.Create(evaluation);
            evaluationDto.Id = evaluation.Id;

            return new ObjectResult(evaluationDto);
        }
    }
}