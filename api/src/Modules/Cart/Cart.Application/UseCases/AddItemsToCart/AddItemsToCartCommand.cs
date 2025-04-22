using Cart.Application.Dtos;
using MediatR;

namespace Cart.Application.UseCases.AddItemsToCart;

public class AddItemsToCartCommand(Guid userId) : IRequest<Unit>
{
    public Guid UserId { get; set; } = userId;
    public IEnumerable<CartItemDto> CartItems { get; set; } = [];
}