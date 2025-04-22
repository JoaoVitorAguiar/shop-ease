using Shared.Entities;

namespace Cart.Domain.Entities;

public class Cart : Entity
{
    public Cart() {}

    public Cart(Guid userId)
    {
        userId = userId;
    }
    public Guid UserId { get; private set; } 
    public List<CartItem> Items { get; set; } = [];
}