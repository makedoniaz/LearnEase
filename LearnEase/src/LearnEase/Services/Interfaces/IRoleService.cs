using LearnEase.Models;

namespace LearnEase.Services.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetUserRolesByUserIdAsync(int userId);
        
        Task<Role> GetRoleByRoleNameAsync(string roleName);
    }
}