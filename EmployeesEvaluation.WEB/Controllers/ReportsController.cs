using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Telerik.Reporting.Cache.File;
using Telerik.Reporting.Services;
using Telerik.Reporting.Services.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace EmployeesEvaluation.WEB.Controllers
{
    [Route("api/[controller]")]
    public class ReportsController : ReportsControllerBase
    {
        public ReportsController(IHostingEnvironment environment)
        {
            var reportsPath =
               Path.Combine(environment.WebRootPath, "Reports");

            this.ReportServiceConfiguration =
               new ReportServiceConfiguration
               {
                   HostAppId = "Html5DemoApp",
                   Storage = new FileStorage(),
                   ReportResolver = new ReportFileResolver(reportsPath),
                   
               };
        }
    }
}