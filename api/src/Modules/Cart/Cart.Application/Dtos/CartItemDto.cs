namespace Cart.Application.Dtos;

public record CartItemDto
{
    public Guid ProductId { get; set; }
    public int Quantity { get;  set; } 
}