using System.Data;
using Dapper;
using Orders.Domain.Entities;
using Orders.Domain.Repositories;

namespace Orders.Infrastructure.Repositories;

public class ProductRepository(IDbConnection dbConnection) : IProductRepository
{
    public async Task<Product?> GetProductByIdAsync(Guid id)
    {
        const string sql = "SELECT * FROM Products WHERE Id = @Id";
        return await dbConnection.QueryFirstOrDefaultAsync<Product>(sql, new { Id = id });
    }

    public async Task<Product?> GetProductBySkuAsync(string sku)
    {
        const string sql = "SELECT * FROM Products WHERE Sku = @Sku";
        return await dbConnection.QueryFirstOrDefaultAsync<Product>(sql, new { Sku = sku });
    }
}