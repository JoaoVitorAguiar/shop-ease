using Authentication.Domain.Entities;

namespace Authentication.Domain.Repository;

public interface IUserCredentialsRepository
{
    Task <UserCredentials?> GetByEmailAsync(string email);
}