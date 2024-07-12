namespace LearnEase.Core.Repositories.Base;

public interface IPutAsync<TId, TEntity>
{
    public Task<int> PutAsync(int id, TEntity entity);
}
