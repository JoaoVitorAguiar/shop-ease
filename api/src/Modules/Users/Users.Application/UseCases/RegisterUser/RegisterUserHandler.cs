using MediatR;
using Shared.ValueObjects;
using Users.Application.Utils;
using Users.Domain.Entities;
using Users.Domain.Exceptions;
using Users.Domain.Repositories;

namespace Users.Application.UseCases.RegisterUser;

public class RegisterUserCommandHandler(IUserRepository userRepository) : IRequestHandler<RegisterUserCommand, Unit>
{
    public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var userWithSameEmail =  await userRepository.GetByEmailAsync(request.Email);
        if (userWithSameEmail != null) throw new UserAlreadyExistsException(request.Email);
        
        var passwordHash = HashService.HashPassword(request.Password);
        var user = new User(
            request.Name,
            new Email(request.Email),
            passwordHash
        );
        
        await userRepository.AddAsync(user); 
        return Unit.Value;
    }
}
