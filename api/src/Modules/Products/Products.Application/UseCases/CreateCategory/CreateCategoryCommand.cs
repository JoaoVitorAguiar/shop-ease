using MediatR;

namespace Products.Application.UseCases.CreateCategory;

public record CreateCategoryCommand(string Name): IRequest<Unit>;