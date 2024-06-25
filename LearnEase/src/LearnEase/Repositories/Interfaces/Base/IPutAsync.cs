namespace LearnEase.Repositories.Interfaces.Base
{
    public interface IPutAsync<TEntity>
    {
        public Task<int> PutAsync(int id, TEntity entity);
    }
}