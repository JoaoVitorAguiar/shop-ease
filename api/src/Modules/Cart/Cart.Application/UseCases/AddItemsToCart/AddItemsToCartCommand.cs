using Cart.Application.UseCases.Dtos;
using MediatR;

namespace Cart.Application.UseCases.AddItemsToCart;

public class AddItemsToCartCommand(Guid userId, Guid cartId) : IRequest<Unit>
{
    public Guid UserId { get; set; } = userId;
    public Guid CartId { get; set; } = cartId;
    public IEnumerable<CartItemDto> CartItems { get; set; } = [];
}