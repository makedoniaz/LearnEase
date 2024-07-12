using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnEase.Models;
using LearnEase.Repositories.Interfaces.Base;

namespace LearnEase.Repositories.Interfaces
{
    public interface ILogRepository : ICreateAsync<Log>
    {
        
    }
}