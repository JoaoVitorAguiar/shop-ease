using Microsoft.EntityFrameworkCore;
using Products.Domain.Entities;
using Products.Domain.Repositories;
using Shared.ValueObjects;

namespace Products.Infrastructure.Repositories;

public class CategoryRepository(ProductDbContext dbContext): ICategoryRepository
{
    
    public Task<Category?> GetByIdAsync(Guid id)
    {
        return dbContext.Categories.Where(u => u.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await dbContext.Categories.ToListAsync();
    }

    public Task AddAsync(Category entity)
    {
        dbContext.Categories.Add(entity);
        return dbContext.SaveChangesAsync();
    }

    public Task UpdateAsync(Category entity)
    {
        dbContext.Categories.Update(entity);
        return dbContext.SaveChangesAsync();
    }

    public Task DeleteAsync(Guid id)
    {
        dbContext.Categories.Remove(dbContext.Categories.SingleOrDefault(u => u.Id == id) ?? throw new InvalidOperationException());
        return dbContext.SaveChangesAsync();
    }

    public Task<Category?> GetByNameAsync(string name)
    {
        return dbContext.Categories.SingleOrDefaultAsync(p => p.Name == name);
    }
}