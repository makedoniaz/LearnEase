using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnEase.Data;
using LearnEase.Models;
using LearnEase.Repositories.Interfaces;

namespace LearnEase.Repositories.EfCore
{
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
}