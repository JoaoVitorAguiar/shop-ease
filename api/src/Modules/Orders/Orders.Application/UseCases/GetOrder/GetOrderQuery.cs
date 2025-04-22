using MediatR;
using Orders.Application.Dtos;

namespace Orders.Application.UseCases.GetOrder;

public record GetOrderQuery(Guid UserId) : IRequest<List<OrderDto>>;
