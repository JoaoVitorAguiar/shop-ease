using Cart.Application.UseCases.Dtos;
using Cart.Domain.Entities;
using Cart.Domain.Exceptions;
using Cart.Domain.Repositories;
using MediatR;

namespace Cart.Application.UseCases.AddItemsToCart;

public class AddItemsToCartHandler(
    ICartItemRepository cartItemRepository,
    ICartRepository cartRepository) : IRequestHandler<AddItemsToCartCommand, Unit>
{
    public async Task<Unit> Handle(AddItemsToCartCommand request, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetByIdAsync(request.CartId);
        if (cart is null) throw new CartNotFoundException(request.CartId);
        
        if (cart.UserId == request.UserId) throw new CartAlreadyExistsException(request.CartId);

        var consolidatedItems = request.CartItems
            .GroupBy(item => item.ProductId)
            .Select(group => new CartItemDto
            {
                ProductId = group.Key,
                Quantity = group.Sum(item => item.Quantity)
            })
            .ToList();

        var cartItems = await cartItemRepository.GetAllByCartIdAsync(request.CartId);

        foreach (var item in consolidatedItems)
        {
            var existingItem = cartItems.FirstOrDefault(ci => ci.ProductId == item.ProductId);

            if (existingItem is null)
            {
                await cartItemRepository.AddAsync(new CartItem(cart.Id, item.ProductId, item.Quantity));
            }
            else
            {
                existingItem.AddQuantity(item.Quantity);
                await cartItemRepository.UpdateAsync(existingItem);
            }
        }

        return default;
    }

}