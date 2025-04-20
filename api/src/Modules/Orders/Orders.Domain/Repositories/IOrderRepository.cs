using Orders.Domain.Entities;
using Shared.Repositories;

namespace Orders.Domain.Repositories;

public interface IOrderRepository: IRepository<Order>
{
}