using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEase.Repositories.Interfaces.Base
{
    public interface IDeleteAsync<TId>
    {
        public Task DeleteAsync(TId entityId);
    }
}