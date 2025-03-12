using Shared.Repositories;

namespace Shared.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
    Task SaveChanges();
}