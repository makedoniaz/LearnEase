using LearnEase.Dtos;
using LearnEase.Models;
using LearnEase.Repositories.Interfaces;
using LearnEase.Services.Interfaces;

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

    public async Task<User> FindUserByCredentialsAsync(LoginDto userCredentials)
    {
        var foundUser = await userRepository.FindByCredentialsAsync(userCredentials);

        if (foundUser is null)
            throw new ArgumentException("Invalid credentials!");

        return foundUser;
    }

    public async Task CreateUserAsync(RegistrationDto registrationDto)
    {
        var newUser = new User() {
            Name = registrationDto.Name,
            Email = registrationDto.Email,
            Password = registrationDto.Password
        };

        var changesCount = await userRepository.CreateAsync(newUser);

        if (changesCount == 0)
            throw new Exception("User creation didn't apply!");
    }

    public async Task<bool> HasAnyUsersRegistered()
    {
        return await userRepository.IsNotEmptyAsync();
    }
}
