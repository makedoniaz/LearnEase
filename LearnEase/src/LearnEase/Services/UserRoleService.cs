using LearnEase.Models;
using LearnEase.Services.Interfaces;

namespace LearnEase.Services;

public class UserRoleService : IUserRoleService
{
    private readonly IUserRoleRepository userRoleRepository;

    private readonly IRoleService roleService;

    public UserRoleService(IUserRoleRepository userRoleRepository, IRoleService roleService)
    {
        this.userRoleRepository = userRoleRepository;
        this.roleService = roleService;
    }

    public async Task SetUserRoleAsync(long userId, string roleName)
    {
        var role = await roleService.GetRoleByRoleNameAsync(roleName);

        var newUserRole = new UserRole() {
            UserId = userId,
            RoleId = role.Id
        };

        await this.userRoleRepository.CreateAsync(newUserRole);
    }

    public async Task DeleteUserRoleAsync(int userRoleId)
    {
        var changesCount = await this.userRoleRepository.DeleteAsync(userRoleId);

        if (changesCount == 0)
            throw new Exception("User role delete didn't apply!");
    }

    public Task<IEnumerable<UserRole>> GetAllUserRolesAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<List<Role>> GetUserRolesByUserId(long userId)
    {
        var roles = await userRoleRepository.GetRolesByUserId(userId);

        if (roles is null)
            throw new ArgumentException("This user doesn't have roles!");

        return roles;
    }

    public Task PutUserRoleAsync(int userRoleId, UserRole userRole)
    {
        throw new NotImplementedException();
    }

    public async Task SetDefaultUserRoleAsync(long userId)
    {
        var defaultRole = await roleService.GetRoleByRoleNameAsync("User");

        var userRole = new UserRole() {
            UserId = userId,
            RoleId = defaultRole.Id
        };

        await this.userRoleRepository.CreateAsync(userRole);
    }
}
