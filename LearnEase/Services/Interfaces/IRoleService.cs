using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnEase.Models;

namespace LearnEase.Services.Interfaces
{
    public interface IRoleService
    {
        public Task<IEnumerable<Role>> GetUserRolesByUserIdAsync(int userId);

        public Task AddRoleToUserById(Role role, int userId);
    }
}