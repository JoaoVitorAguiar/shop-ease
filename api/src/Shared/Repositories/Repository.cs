using Microsoft.EntityFrameworkCore;

namespace Shared.Repositories;

public class Repository<TEntity, TDbContext>(TDbContext context) : IRepository<TEntity>
    where TEntity : class
    where TDbContext : DbContext
{
    private readonly TDbContext _context = context;
    private readonly DbSet<TEntity> _set = context.Set<TEntity>();

    public async Task<TEntity> GetById(Guid id)
    {
        return await _set.FindAsync(id);
    }

    public async Task<IEnumerable<TEntity>> GetAll()
    {
        return await _set.ToListAsync();
    }

    public void Add(TEntity entity)
    {
        _set.Add(entity);
    }

    public void Update(TEntity entity)
    {
        _set.Update(entity);
    }

    public void Delete(Guid id)
    {
        var entity = _set.Find(id);
        if (entity != null)
        {
            _set.Remove(entity);
        }
    }
}