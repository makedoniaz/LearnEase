using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEase.Repositories.Interfaces.Base
{
    public interface IGetById<TEntity, TId>
    {
        Task<TEntity> GetById(TId id);
    }
}