
using Cart.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cart.Infrastructure.Repositories;

public class CartRepository(CartDbContext dbContext): ICartRepository
{
    public Task<Domain.Entities.Cart?> GetByIdAsync(Guid id)
    {
        return dbContext.Carts.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Domain.Entities.Cart>> GetAllAsync()
    {
        return await dbContext.Carts.ToListAsync();
    }

    public async Task AddAsync(Domain.Entities.Cart entity)
    {
        await dbContext.Carts.AddAsync(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Domain.Entities.Cart entity)
    {
        dbContext.Carts.Update(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        dbContext.Carts.Remove(dbContext.Carts.SingleOrDefault(c => c.Id == id) ?? throw new InvalidOperationException());
        await dbContext.SaveChangesAsync();
    }

    public Task<Domain.Entities.Cart?> GetByUserIdAsync(Guid userId)
    {
        return dbContext.Carts.FirstOrDefaultAsync(c => c.UserId == userId);
    }

    public Task<Domain.Entities.Cart?> GetByUserId(Guid userId)
    {
        return dbContext.Carts.FirstOrDefaultAsync(c => c.UserId == userId);
    }
}