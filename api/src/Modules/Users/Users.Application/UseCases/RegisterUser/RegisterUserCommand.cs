using MediatR;

namespace Users.Application.UseCases.RegisterUser;

public record RegisterUserCommand(string Name, string Email, string Password): IRequest<Unit> { }