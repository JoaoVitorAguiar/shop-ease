using MediatR;
using Users.Application.Utils;
using Users.Domain.Exceptions;
using Users.Domain.Repositories;

namespace Users.Application.UseCases.AuthenticateUser;

public class AuthenticateUserHandler(IUserRepository userRepository): IRequestHandler<AuthenticateUserCommand, bool>
{
    public async Task<bool> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(request.Email);
        if (user == null) throw new InvalidCredentialsException();
        return HashService.VerifyPassword(request.Password, user.PasswordHash);
    }
}