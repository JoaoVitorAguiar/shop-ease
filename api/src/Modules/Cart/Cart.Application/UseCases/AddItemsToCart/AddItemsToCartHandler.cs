using Cart.Application.Dtos;
using Cart.Domain.Entities;
using Cart.Domain.Exceptions;
using Cart.Domain.Repositories;
using MediatR;

namespace Cart.Application.UseCases.AddItemsToCart;

public class AddItemsToCartHandler(
    ICartItemRepository cartItemRepository,
    ICartRepository cartRepository,
    IProductRepository productRepository) : IRequestHandler<AddItemsToCartCommand, Unit>
{
    public async Task<Unit> Handle(AddItemsToCartCommand request, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetByUserIdAsync(request.UserId);
        if (cart is null) throw new CartNotFoundException();
        
        var consolidatedItems = request.CartItems
            .GroupBy(item => item.ProductId)
            .Select(group => new CartItemDto
            {
                ProductId = group.Key,
                Quantity = group.Sum(item => item.Quantity)
            })
            .ToList();

        var cartItems = await cartItemRepository.GetAllByCartIdAsync(cart.Id);
        
        var toAdd = new List<CartItem>();
        var toUpdate = new List<CartItem>();
        
        foreach (var item in consolidatedItems)
        {
            var productExists = await productRepository.ExistsAsync(item.ProductId);
            if (!productExists)
                continue;

            var existingItem = cartItems.FirstOrDefault(ci => ci.ProductId == item.ProductId);

            if (existingItem is null)
            {
                toAdd.Add(new CartItem(cart.Id, item.ProductId, item.Quantity));
            }
            else
            {
                existingItem.AddQuantity(item.Quantity);
                toUpdate.Add(existingItem);
            }
        }

        if (toAdd.Any())
            await cartItemRepository.AddRangeAsync(toAdd);

        if (toUpdate.Any())
            await cartItemRepository.UpdateRangeAsync(toUpdate);

        return default;
    }

}