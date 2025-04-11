using Shared.Entities;

namespace Cart.Domain.Entities;

public class Cart(Guid userId) : Entity
{
    public Guid UserId { get; private set; } = userId;
    public ICollection<CartItem> Items { get; private set; } = new List<CartItem>();

}