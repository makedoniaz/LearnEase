namespace LearnEase.Repositories.Interfaces.Base
{
    public interface ICreateAsync<TEntity>
    {
        Task CreateAsync(TEntity entity);
    }
}