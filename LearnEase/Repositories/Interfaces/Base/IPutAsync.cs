namespace LearnEase.Repositories.Interfaces.Base
{
    public interface IPutAsync<TEntity>
    {
        public Task PutAsync(int id, TEntity entity);
    }
}