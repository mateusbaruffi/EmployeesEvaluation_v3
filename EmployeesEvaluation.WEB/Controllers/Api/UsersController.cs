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
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace EmployeesEvaluation.WEB.Controllers.Api
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUserService _userService;
        private readonly IUserRelationService _userRelationService;
        private readonly ILogger _logger;

        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IUserRelationService userRelationService, IUserService userService, ILogger<UsersController> logger)
        {
            this._userRelationService = userRelationService;
            this._userService = userService;
            this._logger = logger;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        [HttpPost("List")]
        public JsonResult List([DataSourceRequest]DataSourceRequest request)
        {
            var result = _userService.All().Select(Mapper.Map<ApplicationUser, UserDto>);
            var dsResult = result.ToDataSourceResult(request);
            return Json(dsResult);
        }

        [HttpGet("ListDepartmentManagers")]
        public IActionResult ListDepartmentManagers([DataSourceRequest]DataSourceRequest request, string text)
        {
            var result = _userService.FindBy(u => u.UserType == UserType.DM);
            return Json(result.ToList());
        }

        [HttpPost("GetEmployeesByDepartmentManagerId")]
        public JsonResult GetEmployeesByDepartmentManagerId(string departmentManagerId)
        {

            var result = _userRelationService.GetEmployeesByDepartmentManagerId(departmentManagerId).Select(Mapper.Map<ApplicationUser, UserDto>);
           
            return Json(result.ToList());
        }

        [HttpPost("Delete")]
        public async Task<ActionResult> Delete([DataSourceRequest] DataSourceRequest request, UserDto userDto)
        {
            var user = Mapper.Map<UserDto, ApplicationUser>(userDto);

            ApplicationUser userManager = await _userManager.FindByIdAsync(userDto.Id);

            string currentRoleName = _userManager.GetRolesAsync(userManager).Result.Single();
            string currentRoleId = _roleManager.Roles.Single(r => r.Name == currentRoleName).Id;

            // remove relation between use and role before delete user
            IdentityResult roleResult = await _userManager.RemoveFromRoleAsync(userManager, currentRoleName);

            _userService.Delete(user.Id);

            return Json(new { Data = userDto });
        }

        [HttpGet("GetHrDmManagers")]
        public JsonResult GetHrDmManagers()
        {
            var ownerships = _userService.FindBy(u => u.UserType == UserType.HRM || u.UserType == UserType.DM).Select(Mapper.Map<ApplicationUser, UserDto>);
            return Json(ownerships);
        }

        [HttpGet("GetDepartmentManagers")]
        public JsonResult GetDepartmentManagers()
        {
            var ownerships = _userService.FindBy(u => u.UserType == UserType.DM).Select(Mapper.Map<ApplicationUser, UserDto>);
            return Json(ownerships);
        }

        


    }
}