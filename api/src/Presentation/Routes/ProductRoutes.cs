using MediatR;
using Products.Application.UseCases.CreateCategory;
using Products.Application.UseCases.CreateProduct;
using Users.Application.UseCases.AuthenticateUser;
using Users.Application.UseCases.RegisterUser;

namespace Presentation.Routes;

public static class ProductRoutes
{
    public static void MapProductRoutes(this IEndpointRouteBuilder app)
    {
        var userGroup = app.MapGroup("/products");

        userGroup.MapPost("/", async (IMediator mediator, CreateProductCommand command) =>
        {
            await mediator.Send(command);
            return Results.Created("", "Produto criado");
        });
        
        userGroup.MapPost("/category", async (IMediator mediator, CreateCategoryCommand command) =>
        {
            await mediator.Send(command);
            return Results.Created("", "Produto criado");
        });
    }
}