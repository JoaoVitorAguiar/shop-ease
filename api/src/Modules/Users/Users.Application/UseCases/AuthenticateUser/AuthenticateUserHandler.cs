using MediatR;
using Users.Application.Utils;
using Users.Domain.Repositories;

namespace Users.Application.UseCases.AuthenticateUser;

public class AuthenticateUserHandler(IUserRepository userRepository): IRequestHandler<AuthenticateUserCommand, bool>
{
    public async Task<bool> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByEmailAsync(request.Email);
        if (user == null)
        {
            Console.WriteLine("User not found");
            return false;
        }
        return HashService.VerifyPassword(request.Password, user.PasswordHash);
    }
}