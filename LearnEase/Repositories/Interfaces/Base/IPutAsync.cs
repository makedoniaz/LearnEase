using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEase.Repositories.Interfaces.Base
{
    public interface IPutAsync<TEntity>
    {
        public Task PutAsync(int id, TEntity entity);
    }
}