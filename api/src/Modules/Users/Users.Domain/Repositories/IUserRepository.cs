using Shared.Repositories;
using Users.Domain.Entities;

namespace Users.Domain.Repositories;

public interface IUserRepository: IRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
}