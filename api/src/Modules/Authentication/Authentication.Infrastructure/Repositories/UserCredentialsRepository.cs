using Authentication.Domain.Entities;
using Authentication.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Infrastructure.Repositories;

public class UserCredentialsRepository(AuthDbContext dbContext): IUserCredentialsRepository
{
    public Task<UserCredentials?> GetByEmailAsync(string email)
    {
        return dbContext.UserCredentials.FirstOrDefaultAsync(u => u.Email == email);
    }
}