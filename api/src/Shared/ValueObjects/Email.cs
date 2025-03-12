using System.Text.RegularExpressions;

namespace Shared.ValueObjects;

public sealed class Email : IEquatable<Email>
{
    public string Value { get; }

    public Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email cannot be null or empty.");

        if (!IsValidEmail(value))
            throw new ArgumentException("Invalid email format.");

        Value = value;
    }

    private static bool IsValidEmail(string email)
    {
        // Regex simples para validar e-mail
        const string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, pattern);
    }

    public override string ToString() => Value;

    public override bool Equals(object obj)
    {
        if (obj is Email other)
            return Value == other.Value;

        return false;
    }

    public bool Equals(Email other)
    {
        return Value == other?.Value;
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public static bool operator ==(Email left, Email right)
    {
        if (left is null && right is null)
            return true;

        if (left is null || right is null)
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(Email left, Email right)
    {
        return !(left == right);
    }
}