namespace LearnEase.Repositories.Interfaces.Base
{
    public interface IDeleteAsync<TId>
    {
        public Task<int> DeleteAsync(TId entityId);
    }
}