using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnEase.Models;

namespace LearnEase.Services.Interfaces
{
    public interface ILogService
    {
        public Task CreateLogAsync(Log log);
    }
}