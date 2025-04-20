namespace Orders.Domain.Entities;

public class CartItem
{
    public Guid CartItemId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public Product Product { get; set; } = default!;

    public CartItem() { }

    public CartItem(Guid cartItemId, Guid productId, int quantity)
    {
        CartItemId = cartItemId;
        ProductId = productId;
        Quantity = quantity;
    }
}
