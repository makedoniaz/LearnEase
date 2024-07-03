using LearnEase.Data;
using LearnEase.Models;
using LearnEase.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LearnEase.Repositories.EfCore;

public class UserRoleEfCoreRepository : IUserRoleRepository
{
    private readonly LearnEaseDbContext _context;

    public UserRoleEfCoreRepository(LearnEaseDbContext context) => _context = context;
    
    public async Task<IEnumerable<UserRole>> GetAllAsync()
    {
        return await _context.UserRoles.ToListAsync();
    }

    public async Task<int> CreateAsync(UserRole userRole)
    {
        _context.UserRoles.Add(userRole);
        var changedObjectsCount = await _context.SaveChangesAsync();

        return changedObjectsCount;
    }

    public async Task<int> DeleteAsync(int id)
    {
        var userRoleToDelete = _context.UserRoles.FirstOrDefault(ur => ur.Id == id);

        if (userRoleToDelete is null)
            return 0;

        _context.UserRoles.Remove(userRoleToDelete);
        var changedObjectsCount = await _context.SaveChangesAsync();

        return changedObjectsCount;
    }

    public async Task<int> PutAsync(int id, Role role)
    {
        var userRoleToUpdate = _context.UserRoles.FirstOrDefault(ur => ur.Id == id);

        if (userRoleToUpdate is null)
            return 0;
        
        userRoleToUpdate.RoleId = role.Id;

        _context.UserRoles.Update(userRoleToUpdate);     
        var changedObjectsCount = await _context.SaveChangesAsync();

        return changedObjectsCount;
    }

    public async Task<List<Role>> GetRolesByUserId(long userId)
    {
        var roles = from ur in _context.UserRoles
                        join r in _context.Roles on ur.RoleId equals r.Id
                        where ur.UserId == userId
                        select r;


        return await roles.ToListAsync();
    }
}
