namespace Authentication.Domain.Entities;

public class UserCredentials(string email, string passwordHash)
{
    public string Email { get; private set; } = email;
    public string PasswordHash { get; private set; } = passwordHash;
}