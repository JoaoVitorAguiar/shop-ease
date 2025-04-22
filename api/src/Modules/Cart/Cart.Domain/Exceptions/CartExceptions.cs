using Shared.Exceptions;

namespace Cart.Domain.Exceptions;

public class CartAlreadyExistsException(Guid userId)
    : DomainException($"Cart already exists", 409, details: $"A cart with the user id '{userId}' is already registered" );
    
public class CartNotFoundException()
    : DomainException($"A cart with the ID was not found", 404);

public class CartNotFoundByUserException(Guid userId)
    : Exception($"Cart not found for user with ID '{userId}.");
    
