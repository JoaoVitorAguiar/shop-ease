using Cart.Application.UseCases.AddItemsToCart;
using Cart.Application.UseCases.CreateCart;
using Cart.Domain.Repositories;
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
        
        userGroup.MapGet("/", async (IProductRepository productRepository) =>
        {
            var products = await productRepository.GetProductByIdAsync(new Guid("2efbb328-5b14-41d9-8734-4702f215b43f"));
            return Results.Ok(products);
        });
        
        userGroup.MapPost("/cart", async (IMediator mediator, CreateCartCommand command) =>
        {
            await mediator.Send(command);
            return Results.Created("", " criado");
        });
        
        userGroup.MapPost("/cart-item", async (IMediator mediator, AddItemsToCartCommand command) =>
        {
            await mediator.Send(command);
            return Results.Created("", " criado");
        });
    }
}