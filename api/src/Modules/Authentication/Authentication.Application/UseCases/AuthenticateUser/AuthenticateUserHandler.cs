using Authentication.Application.Utils;
using Authentication.Domain.Exceptions;
using Authentication.Domain.Repository;
using Authentication.Domain.Services;
using MediatR;
using Users.Application.UseCases.AuthenticateUser;

namespace Authentication.Application.UseCases.AuthenticateUser;

public class AuthenticateUserHandler(
    IUserCredentialsRepository userCredentialsRepository,
    IJwtProvider jwtProvider): IRequestHandler<AuthenticateUserCommand, string>
{
    public async Task<string> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userCredentialsRepository.GetByEmailAsync(request.Email);
        if (user == null) throw new InvalidCredentialsException();
        
        var passwordsMatch = HashService.VerifyPassword(request.Password, user.PasswordHash);
        if (!passwordsMatch) throw new InvalidCredentialsException();
        return jwtProvider.GenerateToken(user.Id);
    }
}