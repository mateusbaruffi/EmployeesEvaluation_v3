using EmployeesEvaluation.Core.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeesEvaluation.WEB.Models.AccountViewModels
{
    public class RegisterViewModel
    {

        public string Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public List<SelectListItem> ApplicationRoles { get; set; }

        [Display(Name = "Role")]
        public string ApplicationRoleId { get; set; }

        //public IEnumerable<string> DepartmentManagerIds { get; set; }
        [Display(Name = "Department Manager")]
        public string[] DepartmentManagerIds { get; set; }

        public List<SelectListItem> ApplicationUsers { get; set; }

        [Required]
        public UserType UserType { get; set; }


    }
}
