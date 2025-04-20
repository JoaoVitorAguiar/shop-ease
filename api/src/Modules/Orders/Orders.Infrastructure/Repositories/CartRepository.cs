using System.Data;
using Dapper;
using Orders.Domain.Entities;
using Orders.Domain.Repositories;

namespace Orders.Infrastructure.Repositories;

public class CartRepository(IDbConnection dbConnection) : ICartRepository
{
    public async Task<Cart?> GetCartByUserIdAsync(Guid userId)
    {
        const string sql = @"
            SELECT
                -- ==== Cart ====
                c.id          AS Id,
                c.user_id     AS UserId,

                -- ==== CartItem ====
                ci.id         AS CartItemId,
                ci.product_id AS ProductId,
                ci.quantity   AS Quantity,

                -- ==== Product ====
                p.id          AS Id,
                p.name        AS Name,
                p.price       AS Price,
                p.sku         AS Sku,
                p.stock       AS Stock
            FROM ""carts"" c
            LEFT JOIN ""cart_items"" ci ON ci.cart_id = c.id
            LEFT JOIN ""products""    p  ON p.id        = ci.product_id
            WHERE c.user_id = @UserId
        ";
        
        var cartDictionary = new Dictionary<Guid, Cart>();
        var result = await dbConnection.QueryAsync<Cart, CartItem, Product, Cart>(
            sql,
            map: (cart, item, product) =>
            {
                if (!cartDictionary.TryGetValue(cart.Id, out var currentCart))
                {
                    currentCart = cart;
                    currentCart.Items = new List<CartItem>();
                    cartDictionary.Add(cart.Id, currentCart);
                }
                
                if (item is { CartItemId: var _, ProductId: var _ })
                {
                    item.Product = product;
                    currentCart.Items.Add(item);
                }

                return currentCart;
            },
            param: new { UserId = userId },
            splitOn: "CartItemId,Id"
        );

        return result.FirstOrDefault();
    }

    public async Task ClearCartAsync(Guid cartId)
    {
        const string sql = @"DELETE FROM ""cart_items"" WHERE cart_id = @CartId";
        await dbConnection.ExecuteAsync(sql, new { CartId = cartId });
    }
}
