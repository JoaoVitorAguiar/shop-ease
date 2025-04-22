
using Cart.Domain.Entities;
using Cart.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cart.Infrastructure.Repositories;

public class CartItemRepository(CartDbContext dbContext): ICartItemRepository
{
    public Task<CartItem?> GetByIdAsync(Guid id)
    {
        return dbContext.CartItems.SingleOrDefaultAsync(ci => ci.Id == id);
    }
    public async Task<IEnumerable<CartItem>> GetAllAsync()
    {
        return await dbContext.CartItems.ToListAsync();
    }
    public async Task AddAsync(CartItem entity)
    {
        dbContext.CartItems.Add(entity);
        await dbContext.SaveChangesAsync();
    }
    public Task UpdateAsync(CartItem entity)
    {
        dbContext.CartItems.Update(entity);
        return dbContext.SaveChangesAsync();
    }
    public Task DeleteAsync(Guid id)
    {
        dbContext.CartItems.Remove(dbContext.CartItems.SingleOrDefault(ci => ci.Id == id) ?? throw new InvalidOperationException());
        return dbContext.SaveChangesAsync();
    }
    public async Task<IList<CartItem>> GetAllByCartIdAsync(Guid cartId)
    {
        return await dbContext.CartItems.Where(ci => ci.CartId == cartId).ToListAsync();
    }
    public async Task AddRangeAsync(IEnumerable<CartItem> items)
    {
        await dbContext.CartItems.AddRangeAsync(items);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateRangeAsync(IEnumerable<CartItem> items)
    {
        dbContext.CartItems.UpdateRange(items);
        await dbContext.SaveChangesAsync();
    }
}