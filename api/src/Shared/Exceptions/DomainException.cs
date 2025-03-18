namespace Shared.Exceptions;

public abstract class DomainException(string title, int statusCode, object? details = null) : Exception(title)
{
    public int StatusCode { get; } = statusCode;
    public object? Details { get; } = details;
}