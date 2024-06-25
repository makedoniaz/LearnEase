using LearnEase.Data;
using LearnEase.Models;
using LearnEase.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LearnEase.Repositories.EfCore
{
    public class RoleEfCoreRepository : IRoleRepository
    {
        private readonly LearnEaseDbContext _context;

        public RoleEfCoreRepository(LearnEaseDbContext context) => _context = context;

        public async Task<Role?> GetRoleByNameAsync(string roleName) {
            return await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
        }

        public async Task<IEnumerable<Role>> GetRolesByUserIdAsync(int userId)
        {
            var roles = from ur in _context.UserRoles
                        join r in _context.Roles on ur.RoleId equals r.Id
                        where ur.UserId == userId
                        select r;

            return await roles.ToListAsync();
        }

        public async Task<int> AddUserRoleAsync(UserRole userRole) {
            _context.UserRoles.Add(userRole);
            var changedObjectsCount = await _context.SaveChangesAsync();

            return changedObjectsCount;
        }
    }
}