using MediatR;

namespace Products.Application.UseCases.CreateProduct;

public record CreateProductCommand(string Name, string Description, decimal Price, int Stock, string Sku, Guid CategoryId): IRequest<Unit>;