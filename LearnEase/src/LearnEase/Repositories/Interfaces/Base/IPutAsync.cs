namespace LearnEase.Repositories.Interfaces.Base
{
    public interface IPutAsync<TId, TEntity>
    {
        public Task<int> PutAsync(int id, TEntity entity);
    }
}