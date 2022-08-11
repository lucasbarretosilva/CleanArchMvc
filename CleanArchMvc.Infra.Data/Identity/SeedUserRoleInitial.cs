using CleanArchMvc.Domain.Account;
using Microsoft.AspNetCore.Identity;
using System;

namespace CleanArchMvc.Infra.Data.Identity
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedUserRoleInitial(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async void SeedRoles()
        {
            if(!await _roleManager.RoleExistsAsync("User"))
            {
                var role = new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "USER"
                };
                var roleResult = await _roleManager.CreateAsync(role);
            }

            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                var role = new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                };
                var roleResult = await _roleManager.CreateAsync(role);
            }
        }

        public async void SeedUsers()
        {
            if (await _userManager.FindByEmailAsync("usuario@localhost") == null)
            {
                ApplicationUser user = new()
                {
                    UserName = "usuario@localhost",
                    Email = "usuario@localhost",
                    NormalizedUserName = "usuario@localhost",
                    NormalizedEmail = "usuario@localhost",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString(),
                };

                var result = await _userManager.CreateAsync(user, "Mauricio@123");

                if (result.Succeeded)
                    await _userManager.AddToRoleAsync(user, "User");
            }

            if (await _userManager.FindByEmailAsync("admin@localhost") == null)
            {
                ApplicationUser user = new()
                {
                    UserName = "admin@localhost",
                    Email = "admin@localhost",
                    NormalizedUserName = "admin@localhost",
                    NormalizedEmail = "admin@localhost",
                    EmailConfirmed = true,
                    LockoutEnabled = false,
                    SecurityStamp = Guid.NewGuid().ToString(),
                };

                var result = await _userManager.CreateAsync(user, "Mauricio@123");

                if (result.Succeeded)
                    await _userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}
