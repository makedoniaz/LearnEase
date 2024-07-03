using LearnEase.Dtos;
using LearnEase.Models;
using LearnEase.Repositories.Interfaces.Base;

namespace LearnEase.Repositories.Interfaces;

public interface IUserRepository : IGetByIdAsync<User, int>, ICreateAsync<User>, IPutAsync<int, User>
{
    Task<User?> FindByCredentialsAsync(LoginDto user);

    Task<bool> IsNotEmptyAsync();
}
