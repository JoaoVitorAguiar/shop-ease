using Authentication.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Orders.Application.UseCases.CreateOrder;
using Orders.Application.UseCases.GetOrder;

namespace Presentation.Routes;

public static class OrderRoutes
{
    record CreateOrderBody(string ShippingAddress);

    public static void MapOrdersRoutes(this IEndpointRouteBuilder app)
    {
        var orderGroup = app.MapGroup("/orders")
            .WithTags("Orders");

        orderGroup.MapPost("/", async (
            [FromBody] CreateOrderBody shippingAddress,
            IMediator mediator, 
            User user) =>
        {
            var command = new CreateOrderCommand(user.Id, shippingAddress.ShippingAddress);
            await mediator.Send(command);

            return Results.Created("/orders", new { message = "Order successfully created" });
        }).RequireAuthorization();
        
        orderGroup.MapGet("/", async (
            IMediator mediator,
            User user) =>
        {
            var query = new GetOrderQuery(user.Id);
            var orders = await mediator.Send(query);

            if (orders is null || !orders.Any())
                return Results.NotFound(new { message = "No orders found for this user" });

            return Results.Ok(orders);
        }).RequireAuthorization();
    }
}