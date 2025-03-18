using MediatR;
using Shared.ValueObjects;

namespace Users.Application.UseCases.AuthenticateUser;

public sealed record AuthenticateUserCommand(string Email, string Password): IRequest<bool>;