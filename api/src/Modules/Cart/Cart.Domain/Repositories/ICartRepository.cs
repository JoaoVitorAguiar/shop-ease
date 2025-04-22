using Cart.Domain.Entities;
using Shared.Repositories;

namespace Cart.Domain.Repositories;

public interface ICartRepository: IRepository<Entities.Cart>
{
    Task<Entities.Cart?> GetByUserIdAsync(Guid userId);
}