using Orders.Domain.Entities;
using Shared.Repositories;

namespace Orders.Domain.Repositories;

public interface IOrderItemRepository: IRepository<OrderItem>
{
    Task<IList<OrderItem>> GetAllByOrderIdAsync(Guid orderId);
}