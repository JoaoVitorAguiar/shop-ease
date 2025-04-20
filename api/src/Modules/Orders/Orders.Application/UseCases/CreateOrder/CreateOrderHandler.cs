using MediatR;
using Orders.Domain.Entities;
using Orders.Domain.Exceptions;
using Orders.Domain.Repositories;

namespace Orders.Application.UseCases.CreateOrder;

public class CreateOrderHandler(
    ICartRepository cartRepository,
    IOrderRepository orderRepository
) : IRequestHandler<CreateOrderCommand, Order>
{
    public async Task<Order> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetCartByUserIdAsync(request.CustomerId);

        if (cart is null)
            throw new CartNotFoundByUserException(request.CustomerId);

        if (!cart.Items.Any())
            throw new CartIsEmptyException(request.CustomerId);

        var order = new Order(request.CustomerId, request.ShippingAddress);

        foreach (var cartItem in cart.Items)
        {
            var orderItem = new OrderItem(
                cartItem.ProductId,
                order.Id,
                cartItem.Product.Price,
                cartItem.Quantity
            );

            order.AddItem(orderItem);
        }

        await orderRepository.AddAsync(order);
        await cartRepository.ClearCartAsync(cart.Id);

        return order;
    }
}