namespace LearnEase.Repositories.Interfaces.Base
{
    public interface ICreateAsync<TEntity>
    {
        Task<int> CreateAsync(TEntity entity);
    }
}