using Products.Domain.Entities;
using Shared.Repositories;

namespace Products.Domain.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<Product?> GetBySkuAsync(string sku);
    Task<Product?> GetByNameAsync(string name);
}