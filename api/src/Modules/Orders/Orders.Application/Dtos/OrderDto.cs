using Cart.Application.Dtos;

namespace Orders.Application.Dtos;

public record OrderDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string ShippingAddress { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public List<OrderItemDto> Items { get; set; } = [];
}