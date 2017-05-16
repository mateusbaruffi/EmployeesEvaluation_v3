﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EmployeesEvaluation.WEB.Controllers
{
    [Authorize(Roles = "HRM, EMP" )]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "HRM")]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
