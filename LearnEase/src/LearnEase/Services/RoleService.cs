using LearnEase.Models;
using LearnEase.Repositories.Interfaces;
using LearnEase.Services.Interfaces;

namespace LearnEase.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository roleRepository;
        
        public RoleService(IRoleRepository roleRepository) {
            this.roleRepository = roleRepository;
        }

        public async Task<Role> GetRoleByRoleNameAsync(string roleName)
        {
            var role = await roleRepository.GetRoleByNameAsync(roleName);

            if (role is null)
                throw new ArgumentException("Role doesn't exist");

            return role;
        }

        public async Task<IEnumerable<Role>> GetUserRolesByUserIdAsync(int userId)
        {
            return await this.roleRepository.GetRolesByUserIdAsync(userId);
        }
    }
}