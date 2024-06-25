using LearnEase.Models;

namespace LearnEase.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetRolesByUserIdAsync(int userId);
        
        Task<int> AddUserRoleAsync(UserRole userRole);

        Task<Role?> GetRoleByNameAsync(string roleName);
    }
}