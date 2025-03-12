using MediatR;
using Shared.UnitOfWork;
using Shared.ValueObjects;
using Users.Application.Utils;
using Users.Domain.Entities;

namespace Users.Application.UseCases.RegisterUser;

public class RegisterUserCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<RegisterUserCommand, Unit>
{
    public async Task<Unit> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var passwordHash = HashService.HashPassword(request.Password);
        
        var user = new User(
            request.Name,
            new Email(request.Email),
            passwordHash
        );

        var userRepository = unitOfWork.GetRepository<User>();
        userRepository.Add(user);
        await unitOfWork.SaveChanges(); 

        return Unit.Value;
    }
}
