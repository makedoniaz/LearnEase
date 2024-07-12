namespace LearnEase.Repositories.Interfaces.Base
{
    public interface IGetByIdAsync<TEntity, TId>
    {
        Task<TEntity?> GetByIdAsync(TId id);
    }
}