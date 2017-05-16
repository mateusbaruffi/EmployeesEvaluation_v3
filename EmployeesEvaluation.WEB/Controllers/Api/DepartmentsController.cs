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
    [Route("api/[controller]")]
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger _logger;

        public DepartmentsController(IDepartmentService departmentService, ILogger<DepartmentsController> logger)
        {
            this._departmentService = departmentService;
            this._logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<DepartmentDto> GetDepartments()
        {
            return _departmentService.All().Select(Mapper.Map<Department, DepartmentDto>);
        }

        [HttpPost("List")]
        public JsonResult List([DataSourceRequest]DataSourceRequest request)
        {
            var result = _departmentService.All().Select(Mapper.Map<Department, DepartmentDto>);
            var dsResult = result.ToDataSourceResult(request);
            return Json(dsResult);
        }

        [HttpPost("Create")]
        public ActionResult Create([DataSourceRequest] DataSourceRequest request, DepartmentDto departmentDto)
        {
            var department = Mapper.Map<DepartmentDto, Department>(departmentDto);

            if (department != null)
            {     
                _departmentService.Create(department);
                departmentDto.Id = department.Id;
            }
            return Json(new { Data = departmentDto });
            //return Json(new[] { "Data" = departmentDto }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost("Update")]
        public ActionResult Update([DataSourceRequest] DataSourceRequest request, DepartmentDto departmentDto)
        {
            var department = Mapper.Map<DepartmentDto, Department>(departmentDto);

            if (department != null)
                _departmentService.Update(department);

            return Json(new { Data = departmentDto });
        }

        [HttpPost("Delete")]
        public ActionResult Delete([DataSourceRequest] DataSourceRequest request, DepartmentDto departmentDto)
        {
            var department = Mapper.Map<DepartmentDto, Department>(departmentDto);

            _departmentService.Delete(department.Id);

            return Json(new { Data = departmentDto });
        }

    }
}