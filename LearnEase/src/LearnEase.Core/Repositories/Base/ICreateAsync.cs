namespace LearnEase.Core.Repositories.Base;

public interface ICreateAsync<TEntity>
{
    Task<int> CreateAsync(TEntity entity);
}
