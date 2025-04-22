using Cart.Application.Dtos;
using MediatR;

namespace Cart.Application.UseCases.GetCart;

public record GetCartQuery(Guid UserId) : IRequest<CartDto>;
