using Shared.Entities;

namespace Authentication.Domain.Entities;

public class UserCredentials(string email, string passwordHash)
{
    public Guid Id { get; private set; }
    public string Email { get; private set; } = email;
    public string PasswordHash { get; private set; } = passwordHash;
}