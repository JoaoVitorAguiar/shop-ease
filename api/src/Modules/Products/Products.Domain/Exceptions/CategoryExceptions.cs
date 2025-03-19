using Shared.Exceptions;

namespace Products.Domain.Exceptions;

public class CategoryWithNameAlreadyExistsException(string name)
    : DomainException(
        "Category already exists",
        409,
        $"A category with the name '{name}' is already registered."
    );

public class CategoryNotFoundException(Guid id)
    : DomainException($"A user with the ID '{id}' was not found", 404);
    
