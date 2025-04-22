namespace Cart.Application.Dtos;

public record CartDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<CartItemDto> Items { get; set; } = new();
}