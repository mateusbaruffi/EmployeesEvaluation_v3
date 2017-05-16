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


namespace EmployeesEvaluation.WEB.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/Seasons")]
    public class SeasonsController : Controller
    {

        private readonly ISeasonService _seasonService;
        private readonly ILogger _logger;

        public SeasonsController(ISeasonService seasonService, ILogger<SeasonsController> logger)
        {
            this._seasonService = seasonService;
            this._logger = logger;
        }

        [HttpGet("GetAll")]
        public JsonResult GetAll()
        {
            var result = _seasonService.All().Select(Mapper.Map<Season, SeasonDto>);
            return Json(result);
        }

        [HttpPost("List")]
        public JsonResult List([DataSourceRequest]DataSourceRequest request)
        {
            var result = _seasonService.All().Select(Mapper.Map<Season, SeasonDto>);
            var dsResult = result.ToDataSourceResult(request);
            return Json(dsResult);
        }

        [HttpPost("Create")]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, SeasonDto seasonDto)
        {
            var season = Mapper.Map<SeasonDto, Season>(seasonDto);

            if (season != null)
            {
                _seasonService.Create(season);
                seasonDto.Id = season.Id;
            }

            return Json(new { Data = seasonDto });
        }

        [HttpPost("Update")]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, SeasonDto seasonDto)
        {
            var season = Mapper.Map<SeasonDto, Season>(seasonDto);

            if (season != null)
                _seasonService.Update(season);

            return Json(new { Data = seasonDto });
        }

        [HttpPost("Delete")]
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, SeasonDto seasonDto)
        {
            var season = Mapper.Map<SeasonDto, Season>(seasonDto);

            _seasonService.Delete(season.Id);

            return Json(new { Data = seasonDto });
        }

    }
}