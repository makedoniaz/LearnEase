using LearnEase.Data;
using LearnEase.Dtos;
using LearnEase.Models;
using LearnEase.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LearnEase.Repositories.EfCore;

public class UserEfCoreRepository : IUserRepository
{
    private readonly LearnEaseDbContext _context;

    public UserEfCoreRepository(LearnEaseDbContext context) => _context = context;
    
    public async Task<int> CreateAsync(User user)
    {
        _context.Users.Add(user);
        var changedObjectsCount = await _context.SaveChangesAsync();

        return changedObjectsCount;
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users.FirstOrDefaultAsync((u) => u.Id == id);
    }

    public async Task<User?> FindByCredentialsAsync(LoginDto credentials)
    {
        return await _context.Users.FirstOrDefaultAsync((u) => u.Name == credentials.Login && u.Password == credentials.Password);
    }

    public async Task<int> PutAsync(int id, User user)
    {
        var userToUpdate = _context.Users.FirstOrDefault(u => u.Id == id);

            if (userToUpdate is null)
                return 0;
            
            userToUpdate.Name = user.Name;
            userToUpdate.Email = user.Email;
            userToUpdate.Password = user.Password;

            _context.Users.Update(userToUpdate);
            var changedObjectsCount = await _context.SaveChangesAsync();

            return changedObjectsCount;
    }
}