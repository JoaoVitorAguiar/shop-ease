using System.Data;
using Cart.Domain.Entities;
using Cart.Domain.Repositories;
using Dapper;

namespace Cart.Infrastructure.Repositories;

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

    public async Task<bool> ExistsAsync(Guid id)
    {
        const string sql = "SELECT 1 FROM Products WHERE Id = @Id";
        var result = await dbConnection.ExecuteScalarAsync<int?>(sql, new { Id = id });
        return result.HasValue;
    }
}