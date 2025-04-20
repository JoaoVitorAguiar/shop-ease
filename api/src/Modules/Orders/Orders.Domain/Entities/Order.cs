using Orders.Domain.Enums;
using Shared.Entities;
using Shared.ValueObjects;

namespace Orders.Domain.Entities;

public class Order(Guid customerId, string shippingAddress) : Entity
{
    public Guid CustomerId { get; private set; } = customerId;
    public string ShippingAddress { get; private set; } = shippingAddress;
    public OrderStatus Status { get; private set; } = OrderStatus.Pending;
    public decimal Total => Items.Sum(item => item.Price * item.Quantity);
    public IList<OrderItem> Items { get; set; } = [];
    
    public void AddItem(OrderItem item) => Items.Add(item);
    public void UpdateStatus(OrderStatus newStatus) => Status = newStatus;
}