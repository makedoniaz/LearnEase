using LearnEase.Core.Dtos;
using LearnEase.Core.Models;
using Microsoft.AspNetCore.Http;

namespace LearnEase.Core.Services;

public interface IIdentityService
{
    Task<User> FindUserById(string userId);

    Task SignInAsync(LoginDto loginDto);

    Task RegisterAsync(RegistrationDto registrationDto);

    Task SetDefaultAvatarAsync(string userName);

    Task SignOutAsync();

    Task SetAvatarAsync(string userId, IFormFile? avatar);

}
