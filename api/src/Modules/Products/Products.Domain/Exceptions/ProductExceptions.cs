using Shared.Exceptions;

namespace Products.Domain.Exceptions;

public class ProductWithSkuAlreadyExistsException(string sku)
    : DomainException(
        "Product already exists",
        409,
        $"A product with the SKU '{sku}' is already registered."
    );

public class ProductWithNameAlreadyExistsException(string name)
    : DomainException(
        "Product already exists",
        409,
        $"A product with the name '{name}' is already registered."
    );

public class UserNotFoundException(string id)
    : DomainException($"A user with the ID '{id}' was not found", 404);
    
