using Authentication.Domain.Entities;
using Cart.Application.Dtos;
using Cart.Application.UseCases.AddItemsToCart;
using Cart.Application.UseCases.CreateCart;
using Cart.Application.UseCases.GetCart;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Routes;

public static class CartRoutes
{
    public static void MapCartRoutes(this IEndpointRouteBuilder app)
    {
        var cartGroup = app.MapGroup("/carts")
            .WithTags("Carts");

        cartGroup.MapPost("/", async (IMediator mediator, CreateCartCommand command) =>
        {
            await mediator.Send(command);
            return Results.Created("/carts", new { message = "Cart successfully created" });
        });

        cartGroup.MapGet("/", async (IMediator mediator, User user) =>
        {
            var query = new GetCartQuery(user.Id);
            var cart = await mediator.Send(query);
            return Results.Ok(cart);
        }).RequireAuthorization();

        cartGroup.MapPost("/items", async (
            IMediator mediator, 
            [FromBody] IEnumerable<CartItemDto> cartItems,
            User user) =>
        {
            var command = new AddItemsToCartCommand(user.Id)
            {
                CartItems = cartItems
            };

            await mediator.Send(command);

            return Results.Ok(new { message = "Items successfully added to cart" });
        }).RequireAuthorization();
    }
}