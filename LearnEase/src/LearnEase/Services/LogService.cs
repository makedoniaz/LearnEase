using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnEase.Models;
using LearnEase.Repositories.Interfaces;
using LearnEase.Services.Interfaces;

namespace LearnEase.Services
{
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
}