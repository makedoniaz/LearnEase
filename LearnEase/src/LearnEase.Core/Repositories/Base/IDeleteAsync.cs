namespace LearnEase.Core.Repositories.Base;

public interface IDeleteAsync<TId>
{
    public Task<int> DeleteAsync(TId entityId);
}
