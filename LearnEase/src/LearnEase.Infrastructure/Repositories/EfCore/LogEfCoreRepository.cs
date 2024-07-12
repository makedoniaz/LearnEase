using LearnEase.Core.Data;
using LearnEase.Core.Models;
using LearnEase.Core.Repositories;

namespace LearnEase.Infrastructure.Repositories.EfCore;

public class LogEfCoreRepository : ILogRepository
{
    private readonly LearnEaseDbContext _context;

    public LogEfCoreRepository(LearnEaseDbContext context) => _context = context;

    public async Task<int> CreateAsync(Log log)
    {
        await _context.Logs.AddAsync(log);
        var changedObjectsCount = await _context.SaveChangesAsync();

        return changedObjectsCount;
    }
}
