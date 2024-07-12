namespace LearnEase.Repositories.Interfaces.Base
{
    public interface IGetAllAsync<TEntity>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
    }
}