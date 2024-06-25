using LearnEase.Models;
using LearnEase.Repositories.Interfaces;
using LearnEase.Services.Interfaces;

namespace LearnEase.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository roleRepository;

        private readonly IUserRepository userRepository;
        
        public RoleService(IRoleRepository roleRepository, IUserRepository userRepository) {
            this.roleRepository = roleRepository;
            this.userRepository = userRepository;
        }

        public async Task AddRoleToUserById(Role role, int userId)
        {
            var roleToAdd = await roleRepository.GetRoleByNameAsync(role.Name);

            if (roleToAdd is null)
                throw new Exception("This role doesn't exist!");

            var user = await userRepository.GetByIdAsync(userId);

            if (user is null)
                throw new Exception("User not found!");

            var userRole = new UserRole() {
                UserId = user.Id,
                RoleId = roleToAdd.Id
            };

            await roleRepository.AddUserRoleAsync(userRole);
        }

        public async Task<IEnumerable<Role>> GetUserRolesByUserIdAsync(int userId)
        {
            return await this.roleRepository.GetRolesByUserIdAsync(userId);
        }
    }
}