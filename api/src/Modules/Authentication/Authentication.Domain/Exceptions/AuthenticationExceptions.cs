using Shared.Exceptions;

namespace Authentication.Domain.Exceptions;

public class InvalidCredentialsException()
    : DomainException(
        title: "Invalid credentials", 
        statusCode: 401, 
        details: "The provided email or password is incorrect" 
    );