using Microsoft.EntityFrameworkCore;
using Orders.Domain.Entities;
using Orders.Domain.Repositories;

namespace Orders.Infrastructure.Repositories;

public class OrderRepository(OrderDbContext dbContext): IOrderRepository
{
    public Task<Order?> GetByIdAsync(Guid id)
    {
        return dbContext.Orders.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    { 
        return await dbContext.Orders.ToListAsync();
    }

    public async Task AddAsync(Order entity)
    {
        dbContext.Orders.Add(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Order entity)
    {
        dbContext.Orders.Update(entity);
        await dbContext.SaveChangesAsync();    
    }

    public async Task DeleteAsync(Guid id)
    {
        dbContext.Orders.Remove(dbContext.Orders.SingleOrDefault(o => o.Id == id) ?? throw new InvalidOperationException());
        await dbContext.SaveChangesAsync();
    }
}