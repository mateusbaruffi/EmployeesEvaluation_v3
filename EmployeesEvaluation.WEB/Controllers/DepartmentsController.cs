using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmployeesEvaluation.Core.Models;
using EmployeesEvaluation.Services;
using Microsoft.Extensions.Logging;
using DocumentFormat.OpenXml.Vml.Office;
using Microsoft.AspNetCore.Authorization;

namespace EmployeesEvaluation.WEB.Controllers
{
    [Authorize(Roles = "HRM")]
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger _logger;

        public DepartmentsController(IDepartmentService departmentService, ILogger<DepartmentsController> logger)
        {
            this._departmentService = departmentService;
            this._logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult New()
        {
            return View();
        }

        public IActionResult Save(Department department)
        {
            if (!ModelState.IsValid)
                return View();

            _departmentService.Create(department);

            return RedirectToAction("Index", "Departments");
        }

    }
}