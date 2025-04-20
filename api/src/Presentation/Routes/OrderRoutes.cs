using Authentication.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Orders.Application.UseCases.CreateOrder;

namespace Presentation.Routes;

public static class OrderRoutes
{
    record CreateOrderBody(string ShippingAddress);

    public static void MapOrdersRoutes(this IEndpointRouteBuilder app)
    {
        var userGroup = app.MapGroup("/orders");

        userGroup.MapPost("/", async (
            [FromBody] CreateOrderBody shippingAddress,
            IMediator mediator, 
            User user) =>
        {
            var command = new CreateOrderCommand(user.Id, shippingAddress.ShippingAddress);
            await mediator.Send(command);
            return Results.Created("", "Pedido criado");
        }).RequireAuthorization();
        
    }
}