using System.Security.Authentication;
using System.Security.Claims;
using Azure.Identity;
using LearnEase.Core.Dtos;
using LearnEase.Core.Models;
using LearnEase.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LearnEase.Infrastructure.Services;

public class IdentityService : IIdentityService
{
    private readonly SignInManager<User> signInManager;

    private readonly UserManager<User> userManager;


    public IdentityService(SignInManager<User> signInManager, UserManager<User> userManager)
    {
        this.signInManager = signInManager;
        this.userManager = userManager;
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

    public async Task SetDefaultAvatarAsync(string userName) {
        var user = await userManager.FindByNameAsync(userName);

        if (user is null)
            throw new ArgumentNullException();

        user.AvatarPath = "Assets/Avatars/default.png";
    }

    public async Task SetAvatarAsync(string userId, IFormFile? avatar) {
        var user = await userManager.FindByIdAsync(userId);

        if (user is null)
            throw new ArgumentNullException();

        if (avatar is null)
            return;

        var extension = new FileInfo(avatar.FileName).Extension[1..];
        user.AvatarPath = $"Assets/Avatars/{user.Id}.{extension}";

        using var newFileStream = File.Create(user.AvatarPath);
        await avatar.CopyToAsync(newFileStream);
    }

    public async Task SignOutAsync()
    {
        await this.signInManager.SignOutAsync();
    }

    public async Task<User> FindUserById(string userId)
    {
        var user = await this.userManager.FindByIdAsync(userId);

        if (user is null)
            throw new ArgumentNullException();

        return user;
    }
}
