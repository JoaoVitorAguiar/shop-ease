using Shared.Entities;

namespace Cart.Domain.Entities;

public class CartItem(Guid cartId, Guid productId, int quantity)
    : Entity
{
    public Guid CartId { get; private set; } = cartId;
    public Guid ProductId { get; private set; } = productId;
    public int Quantity { get; private set; } = quantity;

    public Cart Cart { get; private set; }

    public void AddQuantity(int quantity)
    {
        Quantity += quantity;
    }
}