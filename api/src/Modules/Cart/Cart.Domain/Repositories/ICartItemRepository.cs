using Cart.Domain.Entities;
using Shared.Repositories;

namespace Cart.Domain.Repositories;

public interface ICartItemRepository: IRepository<CartItem>
{
    Task<IList<CartItem>> GetAllByCartIdAsync(Guid cartId);
}