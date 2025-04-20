using Shared.Entities;

namespace Orders.Domain.Entities;

public class OrderItem(Guid productId, Guid orderId, decimal price, int quantity): Entity
{
    public Guid ProductId { get; private set;} = productId;
    public Guid OrderId { get; private set;} = orderId;
    public decimal Price { get; private set;} = price;
    public int Quantity { get; private set;} = quantity;
    public Order Order { get; set; }
    
    public void UpdateQuantity(int newQuantity) => Quantity = newQuantity;
}