using Microsoft.EntityFrameworkCore;
using Orders.Domain.Entities;
using Orders.Domain.Repositories;
using Shared.Repositories;

namespace Orders.Infrastructure.Repositories;

public class OrderItemRepository(OrderDbContext dbContext): IOrderItemRepository
{
    public Task<OrderItem?> GetByIdAsync(Guid id)
    {
        return dbContext.OrderItems.SingleOrDefaultAsync(oi => oi.Id == id);
    }

    public async Task<IEnumerable<OrderItem>> GetAllAsync()
    {
        return await dbContext.OrderItems.ToListAsync();
    }

    public async Task AddAsync(OrderItem entity)
    {
        dbContext.Add(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(OrderItem entity)
    {
        dbContext.Update(entity);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var orderItem = await GetByIdAsync(id);
        if (orderItem != null)
        {
            dbContext.OrderItems.Remove(orderItem);
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task<IList<OrderItem>> GetAllByOrderIdAsync(Guid orderId)
    {
        return await dbContext.OrderItems.Where(ci => ci.OrderId == orderId).ToListAsync();
    }
}