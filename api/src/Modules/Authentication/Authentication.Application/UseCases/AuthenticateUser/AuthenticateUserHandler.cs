using Authentication.Application.Utils;
using Authentication.Domain.Exceptions;
using Authentication.Domain.Repository;
using MediatR;
using Users.Application.UseCases.AuthenticateUser;

namespace Authentication.Application.UseCases.AuthenticateUser;

public class AuthenticateUserHandler(IUserCredentialsRepository userCredentialsRepository): IRequestHandler<AuthenticateUserCommand, bool>
{
    public async Task<bool> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userCredentialsRepository.GetByEmailAsync(request.Email);
        if (user == null) throw new InvalidCredentialsException();
        return HashService.VerifyPassword(request.Password, user.PasswordHash);
    }
}