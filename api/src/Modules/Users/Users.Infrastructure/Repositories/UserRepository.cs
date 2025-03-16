using Microsoft.EntityFrameworkCore;
using Shared.ValueObjects;
using Users.Domain.Entities;
using Users.Domain.Repositories;

namespace Users.Infrastructure.Repositories;

public class UserRepository(UserDbContext dbContext): IUserRepository
{
    
    public Task<User?> GetByIdAsync(Guid id)
    {
        return dbContext.Users.Where(u => u.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await dbContext.Users.ToListAsync();
    }

    public Task AddAsync(User entity)
    {
        dbContext.Users.Add(entity);
        return dbContext.SaveChangesAsync();
    }

    public Task UpdateAsync(User entity)
    {
        dbContext.Users.Update(entity);
        return dbContext.SaveChangesAsync();
    }

    public Task DeleteAsync(Guid id)
    {
        dbContext.Users.Remove(dbContext.Users.SingleOrDefault(u => u.Id == id) ?? throw new InvalidOperationException());
        return dbContext.SaveChangesAsync();
    }

    public Task<User?> GetByEmailAsync(string email)
    {
        return dbContext.Users
            .Where(u => u.Email == new Email(email)) 
            .FirstOrDefaultAsync(); 
    }
}