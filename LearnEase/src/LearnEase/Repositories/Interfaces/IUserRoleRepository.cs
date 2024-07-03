using LearnEase.Models;
using LearnEase.Repositories.Interfaces.Base;

namespace LearnEase.Services.Interfaces;

public interface IUserRoleRepository : IGetAllAsync<UserRole>, ICreateAsync<UserRole>,
IDeleteAsync<int>, IPutAsync<int, Role>
{
    Task<List<Role>> GetRolesByUserId(long userId);
}
