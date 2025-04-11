using Shared.Exceptions;

namespace Cart.Domain.Exceptions;

public class CartAlreadyExistsException(Guid userId)
    : DomainException($"Cart already exists", 409, details: $"A cart with the user id '{userId}' is already registered" );
    
public class CartNotFoundException(Guid cartId)
    : DomainException($"A cart with the ID '{cartId}' was not found", 404);
    
