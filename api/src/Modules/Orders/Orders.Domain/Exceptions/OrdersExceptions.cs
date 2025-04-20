using Shared.Exceptions;

namespace Orders.Domain.Exceptions;

public class CartNotFoundByUserException(Guid userId)
    : DomainException("Cart not found", 404, details: $"No cart found for user with ID '{userId}'");

public class CartIsEmptyException(Guid userId)
    : DomainException("Cart is empty", 400, details: $"The cart for user '{userId}' has no items");
    
