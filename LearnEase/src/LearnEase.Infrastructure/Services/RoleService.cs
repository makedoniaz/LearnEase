using LearnEase.Core.Dtos;
using LearnEase.Core.Models;
using LearnEase.Core.Services;
using Microsoft.AspNetCore.Identity;

namespace LearnEase.Infrastructure.Services;

public class RoleService : IRoleService
{
    private readonly RoleManager<IdentityRole> roleManager;

    public RoleService(RoleManager<IdentityRole> roleManager)
    {
        this.roleManager = roleManager;
    }

    public async Task SetupRolesAsync()
    {
        List<string> roleNames = ["Admin", "User", "Author"];
        
        foreach (var roleName in roleNames)
        {
            var roleExists = await this.roleManager.RoleExistsAsync(roleName);
            if (!roleExists)
            {
                var role = new IdentityRole(roleName);
                var result = await this.roleManager.CreateAsync(role);
                
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"Error creating role {roleName}: {error.Description}");
                    }
                }
            }
        }
    }
}
