using Products.Domain.Entities;
using Shared.Repositories;

namespace Products.Domain.Repositories;

public interface ICategoryRepository : IRepository<Category>
{
    Task<Category?> GetByNameAsync(string name);
}