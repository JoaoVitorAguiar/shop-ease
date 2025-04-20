using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Authentication.Domain.Entities;
using Cart.Application.UseCases.AddItemsToCart;
using Cart.Application.UseCases.CreateCart;
using Cart.Application.UseCases.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Routes;

public static class CartRoutes
{
    public static void MapCartRoutes(this IEndpointRouteBuilder app)
    {
        var userGroup = app.MapGroup("/carts");

        userGroup.MapPost("/", async (IMediator mediator, CreateCartCommand command) =>
        {
            await mediator.Send(command);
            return Results.Created("", "Carrinho criado");
        });
        
        userGroup.MapPost("/{cartId:guid}/items", async (
            IMediator mediator, 
            Guid cartId,
            [FromBody]
            IEnumerable<CartItemDto> cartItems,
            User user) =>
        {
            var command = new AddItemsToCartCommand(user.Id, cartId)
            {
                CartItems = cartItems
            };
            await mediator.Send(command);
            return Results.Ok();
        }).RequireAuthorization();
    }
}