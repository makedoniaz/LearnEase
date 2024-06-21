using LearnEase.Models;

namespace LearnEase.Services.Interfaces;

public interface IUserService
{
    Task RegistrateUserAsync(User user);

    Task LoginUserAsync(User user);

    Task ChangeUserInfoAsync(int userId, User user);
}
