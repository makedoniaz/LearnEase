using LearnEase.Core.Dtos;

namespace LearnEase.Core.Services;

public interface IIdentityService
{
    Task SignInAsync(LoginDto loginDto);

    Task RegisterAsync(RegistrationDto registrationDto);

    Task SignOutAsync();
}
