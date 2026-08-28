using System.Data;
using GiftoftheGivers.Models;
using Microsoft.AspNetCore.Identity;

namespace GiftoftheGivers.Data
{
    // Seeds the two required roles and one demo Employee account so the
    // Employee Dashboard is reachable immediately without a manual sign-up step.
    public static class DbSeeder
    {
        public static async Task SeedAsync(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

            foreach (var role in new[] { Roles.Employee, Roles.Donor })
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            const string demoEmployeeEmail = "employee@giftofthegivers.org";
            if (await userManager.FindByEmailAsync(demoEmployeeEmail) is null)
            {
                var employee = new ApplicationUser
                {
                    UserName = demoEmployeeEmail,
                    Email = demoEmployeeEmail,
                    FirstName = "Demo",
                    LastName = "Employee",
                    EmailConfirmed = true
                };
                var result = await userManager.CreateAsync(employee, "Employee@123");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(employee, Roles.Employee);
                }
            }
        }
    }
}
