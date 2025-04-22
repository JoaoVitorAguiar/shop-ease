using Cart.Domain.Entities;

namespace Cart.Domain.Repositories;

public interface IProductRepository
{
    Task<Product?> GetProductByIdAsync(Guid id); 
    Task<Product?> GetProductBySkuAsync(string sku); 
    Task<bool> ExistsAsync(Guid id);
}