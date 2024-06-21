using LearnEase.Models;
using LearnEase.Repositories.Interfaces.Base;

namespace LearnEase.Repositories.Interfaces;

public interface IUserRepository : IGetByIdAsync<User, int>, ICreateAsync<User>, IPutAsync<User>
{
    Task<User?> FindByUsernameAsync(string username);
}
