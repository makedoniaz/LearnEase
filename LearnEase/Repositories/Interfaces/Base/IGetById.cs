namespace LearnEase.Repositories.Interfaces.Base
{
    public interface IGetById<TEntity, TId>
    {
        Task<TEntity> GetById(TId id);
    }
}