using LearnEase.Models;

namespace LearnEase.Services.Interfaces;

public interface IUserRoleService
{
    Task<IEnumerable<UserRole>> GetAllUserRolesAsync();
    
    Task SetUserRoleAsync(long userId, string roleName);

    Task PutUserRoleAsync(int userRoleId, UserRole userRole);

    Task DeleteUserRoleAsync(int userRoleId);

    Task SetDefaultUserRoleAsync(long userId);

    Task<List<Role>> GetUserRolesByUserId(long userId);
}
