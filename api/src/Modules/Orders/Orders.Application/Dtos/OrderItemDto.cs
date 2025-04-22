namespace Orders.Application.Dtos;


public record OrderItemDto
{
    public Guid ProductId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
