using System.Data;
using Cart.Application.Dtos;
using Dapper;
using MediatR;
using Orders.Application.Dtos;

namespace Orders.Application.UseCases.GetOrder;

public class GetOrderHandler(IDbConnection dbConnection)
    : IRequestHandler<GetOrderQuery, List<OrderDto>>
{
    public async Task<List<OrderDto>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        const string ordersSql = @"
            SELECT
                id            AS Id,
                customer_id   AS UserId,
                shipping_address AS ShippingAddress,
                status        AS Status,
                created_at    AS CreatedAt
            FROM orders
            WHERE customer_id = @UserId";

        const string itemsSql = @"
            SELECT
                product_id AS ProductId,
                price      AS Price,
                quantity   AS Quantity
            FROM order_items
            WHERE order_id = @OrderId";

        var orders = (await dbConnection.QueryAsync<OrderDto>(
                ordersSql, new { UserId = request.UserId }))
            .ToList();

        if (orders.Count == 0)
            return [];

        foreach (var order in orders)
        {
            var items = await dbConnection.QueryAsync<OrderItemDto>(
                itemsSql, new { OrderId = order.Id });
            order.Items = items.ToList();
        }

        return orders;
    }
}