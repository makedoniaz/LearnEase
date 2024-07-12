using LearnEase.Core.Models;
using LearnEase.Core.Repositories;
using LearnEase.Core.Services;

namespace LearnEase.Infrastructure.Services;

public class LogService : ILogService
{
    private readonly ILogRepository repository;
    
    public LogService(ILogRepository repository) {
        this.repository = repository;
    }
    
    public async Task CreateLogAsync(Log log)
    {
        if (log is null)
            throw new ArgumentNullException(nameof(log));

        var changesCount = await repository.CreateAsync(log);

        if (changesCount == 0)
            throw new Exception("Log creation didn't apply!");
    }
}
