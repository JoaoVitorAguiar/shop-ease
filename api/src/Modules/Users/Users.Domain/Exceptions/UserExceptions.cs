namespace Users.Domain.Exceptions;

public class UserAlreadyExistsException(string email)
    : DomainException($"User already exists", 409, details: $"A user with the email '{email}' is already registered." );
    
public class UserNotFoundException(string id)
    : DomainException($"A user with the ID '{id}' was not found", 404);
    
public class InvalidCredentialsException()
    : DomainException(
        title: "Invalid credentials", 
        statusCode: 401, 
        details: "The provided email or password is incorrect" 
    );