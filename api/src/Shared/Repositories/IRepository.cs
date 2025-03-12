namespace Shared.Repositories;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity?> GetById(Guid id);
    Task<IEnumerable<TEntity>> GetAll();
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Delete(Guid id);
}