using LearnEase.Dtos;
using LearnEase.Models;

namespace LearnEase.Services.Interfaces;

public interface IUserService
{
    Task CreateUserAsync(RegistrationDto registrationDto);

    Task<User> FindUserByCredentialsAsync(LoginDto userCredentials);

    Task ChangeUserInfoAsync(int userId, User user);

    Task<bool> HasAnyUsersRegistered();
}
