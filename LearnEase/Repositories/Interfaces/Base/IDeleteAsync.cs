namespace LearnEase.Repositories.Interfaces.Base
{
    public interface IDeleteAsync<TId>
    {
        public Task DeleteAsync(TId entityId);
    }
}