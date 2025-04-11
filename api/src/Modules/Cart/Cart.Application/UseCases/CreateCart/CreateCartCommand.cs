using MediatR;

namespace Cart.Application.UseCases.CreateCart;

public record CreateCartCommand(Guid UserId): IRequest<Unit>;