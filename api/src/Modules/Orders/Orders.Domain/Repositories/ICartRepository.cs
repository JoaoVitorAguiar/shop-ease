using Orders.Domain.Entities;

namespace Orders.Domain.Repositories;

public interface ICartRepository
{
    Task<Cart?> GetCartByUserIdAsync(Guid userId);
    Task ClearCartAsync(Guid cartId);
}