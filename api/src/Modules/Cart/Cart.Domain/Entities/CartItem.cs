using Shared.Entities;

namespace Cart.Domain.Entities;

public class CartItem : Entity
{
    public CartItem() { }

    public CartItem(Guid cartId, Guid productId, int quantity)
    {
        CartId = cartId;
        ProductId = productId;
        Quantity = quantity;
    }

    public Guid CartId { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }

    public Cart Cart { get; private set; }

    public void AddQuantity(int quantity)
    {
        Quantity += quantity;
    }
}
