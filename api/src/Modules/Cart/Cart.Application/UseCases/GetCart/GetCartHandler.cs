using System.Data;
using Cart.Application.Dtos;
using Cart.Domain.Entities;
using Cart.Domain.Exceptions;
using Dapper;
using MediatR;

namespace Cart.Application.UseCases.GetCart;

public class GetCartHandler(
    IDbConnection dbConnection
) : IRequestHandler<GetCartQuery, CartDto>
{
    public async Task<CartDto> Handle(GetCartQuery request, CancellationToken cancellationToken)
    {
        const string cartSql = @"
            SELECT id AS Id, user_id AS UserId, created_at AS CreatedAt, updated_at AS UpdatedAt
            FROM carts 
            WHERE user_id = @UserId";

        const string itemsSql = @"
            SELECT product_id AS ProductId, quantity AS Quantity
            FROM cart_items 
            WHERE cart_id = @CartId";

        var cart = await dbConnection.QueryFirstOrDefaultAsync<CartDto>(cartSql, new { request.UserId });

        if (cart is null)
            throw new CartNotFoundByUserException(request.UserId);

        var items = (await dbConnection.QueryAsync<CartItemDto>(itemsSql, new { CartId = cart.Id })).ToList();
        cart.Items = items;

        return cart;
    }
}