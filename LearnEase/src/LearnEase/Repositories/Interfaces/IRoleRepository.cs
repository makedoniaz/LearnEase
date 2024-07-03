using LearnEase.Models;
using LearnEase.Repositories.Interfaces.Base;

namespace LearnEase.Repositories.Interfaces
{
    public interface IRoleRepository : IGetAllAsync<Role>
    {
        Task<IEnumerable<Role>> GetRolesByUserIdAsync(int userId);

        Task<Role?> GetRoleByNameAsync(string roleName);
    }
}