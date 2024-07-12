using LearnEase.Core.Models;

namespace LearnEase.Core.Services;

public interface ILogService
{
    public Task CreateLogAsync(Log log);
}
