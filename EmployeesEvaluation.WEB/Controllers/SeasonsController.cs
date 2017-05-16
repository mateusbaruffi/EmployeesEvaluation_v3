using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EmployeesEvaluation.Core.Models;
using EmployeesEvaluation.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace EmployeesEvaluation.WEB.Controllers
{
    [Authorize(Roles = "HRM")]
    public class SeasonsController : Controller
    {

        private readonly ISeasonService _seasonService;
        private readonly ILogger _logger;

        public SeasonsController(ISeasonService seasonService, ILogger<SeasonsController> logger)
        {
            this._seasonService = seasonService;
            this._logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}