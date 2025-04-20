using Orders.Domain.Entities;

namespace Orders.Domain.Repositories;

public interface IProductRepository
{
    Task<Product?> GetProductByIdAsync(Guid id); 
    Task<Product?> GetProductBySkuAsync(string sku); 
}