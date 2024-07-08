using System.Security.Authentication;
using System.Security.Claims;
using Azure.Identity;
using LearnEase.Core.Dtos;
using LearnEase.Core.Models;
using LearnEase.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LearnEase.Infrastructure.Services;

public class IdentityService : IIdentityService
{
    private readonly SignInManager<User> signInManager;

    private readonly UserManager<User> userManager;

    private readonly IRoleService roleService;


    public IdentityService(SignInManager<User> signInManager, UserManager<User> userManager, IRoleService roleService)
    {
        this.signInManager = signInManager;
        this.userManager = userManager;
        this.roleService = roleService;
    }

    public async Task RegisterAsync(RegistrationDto registrationDto)
    {
        bool usersExist = await userManager.Users.AnyAsync();
        string role = usersExist ? "User" : "Admin";

        var user = new User() {
            UserName = registrationDto.Login,
            Email = registrationDto.Email
        };

        var result = await userManager.CreateAsync(
            user,
            password: registrationDto.Password
        );

        if (!result.Succeeded)
            throw new AuthenticationFailedException(string.Join("\n", result.Errors.Select(error => error.Description)));


        await userManager.AddToRoleAsync(user, role);
    }

    public async Task SignInAsync(LoginDto loginDto)
    {
        var user = await this.userManager.FindByNameAsync(loginDto.Login);

        if(user == null)
            throw new InvalidCredentialException("User not found!");

        if (!user.IsActive)
            throw new AuthenticationException("Account is banned!");

        var result = await this.signInManager.PasswordSignInAsync(user, loginDto.Password, true, false);

        if (!result.Succeeded)
            throw new AuthenticationFailedException("Invalid password!");


        var existingClaim = (await userManager.GetClaimsAsync(user))
                .FirstOrDefault(c => c.Type == "IsMuted");

        if (existingClaim is null)
            await userManager.AddClaimAsync(user, new Claim("IsMuted", user.IsMuted.ToString()));
    }

    public async Task SignOutAsync()
    {
        await this.signInManager.SignOutAsync();
    }
}
