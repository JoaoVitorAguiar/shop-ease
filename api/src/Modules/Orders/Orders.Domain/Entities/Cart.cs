using System.Diagnostics;

namespace Orders.Domain.Entities;

public class Cart
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public List<CartItem> Items { get; set; } = new();

    public Cart() { }

    public Cart(Guid userId)
    {
        UserId = userId;
        Items = new List<CartItem>();
    }
}