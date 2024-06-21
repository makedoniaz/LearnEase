using System.Security.Claims;
using LearnEase.Models;
using LearnEase.Repositories.Interfaces;
using LearnEase.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace LearnEase.Services;

public class UserService : IUserService
{
     private readonly IUserRepository userRepository;

    public UserService(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task ChangeUserInfoAsync(int userId, User user)
    {
        var changesCount = await userRepository.PutAsync(userId, user);

        if (changesCount == 0)
            throw new Exception("Feedback change didn't apply!");
    }

    public async Task LoginUserAsync(User user)
    {
        var foundUser = await userRepository.FindByUsernameAsync(user.Name);

        if (foundUser is null)
            throw new ArgumentException("User not found!");

        if (user.Password != foundUser.Password)
            throw new ArgumentException("Incorrect password!");
    }

    public async Task RegistrateUserAsync(User user)
    {
        var changesCount = await userRepository.CreateAsync(user);

        if (changesCount == 0)
            throw new Exception("User creation didn't apply!");
    }
}
