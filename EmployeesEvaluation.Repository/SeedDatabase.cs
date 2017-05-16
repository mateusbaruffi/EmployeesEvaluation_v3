using EmployeesEvaluation.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeesEvaluation.Repository
{
    public class SeedDatabase
    {

        public static EmployeesEvaluationContext context;
        public static UserManager<ApplicationUser> userManager;
        public static RoleManager<IdentityRole> roleManager;
        public static void Initialize(IServiceProvider serviceProvider)
        {
            //context.Database.EnsureCreated();
            roleManager = (RoleManager<IdentityRole>)serviceProvider.GetService(typeof(RoleManager<IdentityRole>));
            userManager = (UserManager<ApplicationUser>)serviceProvider.GetService(typeof(UserManager<ApplicationUser>));
            context = (EmployeesEvaluationContext)serviceProvider.GetService(typeof(EmployeesEvaluationContext));
            Seed();
        }

        private static async void Seed()
        {

            if (!context.AspNetUsers.Any())
            {

                IdentityResult role01 = await roleManager.CreateAsync(new IdentityRole("HRM"));
                IdentityResult role02 = await roleManager.CreateAsync(new IdentityRole("DM"));
                IdentityResult role03 = await roleManager.CreateAsync(new IdentityRole("EMP"));

                var user = new ApplicationUser { UserName = "hrm@hrm.com", Email = "hrm@hrm.com", UserType = UserType.HRM };
                await userManager.CreateAsync(user, "TN#william5");

                IdentityResult roleResult = await userManager.AddToRoleAsync(user, user.UserType.ToString());

                context.SaveChanges();
            }

        }

    }
}
