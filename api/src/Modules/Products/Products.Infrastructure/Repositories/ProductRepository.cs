using Microsoft.EntityFrameworkCore;
using Products.Domain.Entities;
using Products.Domain.Repositories;
using Shared.ValueObjects;

namespace Products.Infrastructure.Repositories;

public class ProductRepository(ProductDbContext dbContext): IProductRepository
{
    
    public Task<Product?> GetByIdAsync(Guid id)
    {
        return dbContext.Products.Where(u => u.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await dbContext.Products.ToListAsync();
    }

    public Task AddAsync(Product entity)
    {
        dbContext.Products.Add(entity);
        return dbContext.SaveChangesAsync();
    }

    public Task UpdateAsync(Product entity)
    {
        dbContext.Products.Update(entity);
        return dbContext.SaveChangesAsync();
    }

    public Task DeleteAsync(Guid id)
    {
        dbContext.Products.Remove(dbContext.Products.SingleOrDefault(u => u.Id == id) ?? throw new InvalidOperationException());
        return dbContext.SaveChangesAsync();
    }

    public Task<Product?> GetBySkuAsync(string sku)
    {
        return dbContext.Products.SingleOrDefaultAsync(p => p.Sku == sku);
    }

    public Task<Product?> GetByNameAsync(string name)
    {
        return dbContext.Products.SingleOrDefaultAsync(p => p.Name == name);
    }
}