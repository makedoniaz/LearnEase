using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEase.Repositories.Interfaces.Base
{
    public interface ICreateAsync<TEntity>
    {
        Task CreateAsync(TEntity entity);
    }
}