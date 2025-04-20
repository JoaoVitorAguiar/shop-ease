using MediatR;
using Orders.Domain.Entities;

namespace Orders.Application.UseCases.CreateOrder;

public record CreateOrderCommand(Guid CustomerId, string ShippingAddress) : IRequest<Order>;
