using Microsoft.AspNetCore.Identity;
using StudentChat.Data.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace StudentChat.DAL.Persistence.Seeding
{
    public class RolesUsersSeeding
    {
        public static async Task SeedRolesAsync(RoleManager<AppUserRole> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(
                    new AppUserRole { Name = "Administrator", NormalizedName = "ADMINISTRATOR".ToUpper() });
            }

            if (!await roleManager.RoleExistsAsync("User"))
            {
                await roleManager.CreateAsync(
                    new AppUserRole { Name = "User", NormalizedName = "USER".ToUpper() });
            }
        }

        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            // Seed Admin User
            var adminUser = new AppUser
            {
                UserName = "DanieliMiamor",
                Email = "adminUser@example.com",
                NormalizedUserName = "ADMINUSER",
                FullName = "DanieliMegaJuk",
                LastSeen = DateTime.Now,
                IsOnline = false,
            };

            if (userManager.Users.All(u => u.UserName != adminUser.UserName))
            {
                var result = await userManager.CreateAsync(adminUser, "Admin123!");
                if (result.Succeeded)
                {
                    // Assign the "Admin" role to the admin user
                    await userManager.AddToRoleAsync(adminUser, "Administrator");
                }
            }

            // Seed Standard User
            var standardUser = new AppUser
            {
                UserName = "NicuMegaJuk",
                Email = "user@example.com",
                NormalizedUserName = "USER",
                FullName = "Nicushor(poate sh greu eu hz)",
                LastSeen = DateTime.Now,
                IsOnline = false,
            };

            if (userManager.Users.All(u => u.UserName != standardUser.UserName))
            {
                var result = await userManager.CreateAsync(standardUser, "User123!");
                if (result.Succeeded)
                {
                    // Assign the "User" role to the standard user
                    await userManager.AddToRoleAsync(standardUser, "User");
                }
            }
        }
    }
}
