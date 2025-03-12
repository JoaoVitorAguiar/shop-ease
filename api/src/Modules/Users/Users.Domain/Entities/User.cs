using Shared.Entities;
using Shared.Enums;
using Shared.ValueObjects;

namespace Users.Domain.Entities;

public class User(string name, Email email, string passwordHash) : Entity
{
    public string Name { get; private set; } = name;
    public UserRole Role { get; private set; } = UserRole.Customer;
    public Email Email { get; private set; } = email;
    public string PasswordHash { get; private set; } = passwordHash;
}