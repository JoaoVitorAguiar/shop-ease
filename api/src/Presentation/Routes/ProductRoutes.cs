using Cart.Application.UseCases.AddItemsToCart;
using Cart.Application.UseCases.CreateCart;
using Cart.Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Products.Application.UseCases.CreateCategory;
using Products.Application.UseCases.CreateProduct;
using Products.Infrastructure;
using Users.Application.UseCases.AuthenticateUser;
using Users.Application.UseCases.RegisterUser;

namespace Presentation.Routes;

public static class ProductRoutes
{
    public static void MapProductRoutes(this IEndpointRouteBuilder app)
    {
        var productGroup = app.MapGroup("/products")
            .WithTags("Products");
        var categoryGroup = app.MapGroup("/categories")
            .WithTags("Categories");

        productGroup.MapPost("/", async (IMediator mediator, CreateProductCommand command) =>
        {
            await mediator.Send(command);
            return Results.Created("", new { message = "Product created successfully" });
        });
        
        productGroup.MapGet("/", async (ProductDbContext dbContext) =>
        {
            var products = await dbContext.Products.ToListAsync();
            return Results.Ok(new { message = "Products retrieved successfully", data = products });
        });
        
        categoryGroup.MapPost("/", async (IMediator mediator, CreateCategoryCommand command) =>
        {
            await mediator.Send(command);
            return Results.Created("", new { message = "Category created successfully" });
        });

        categoryGroup.MapGet("/", async (ProductDbContext dbContext) =>
        {
            var categories = await dbContext.Categories.ToListAsync();
            return Results.Ok(new { message = "Categories retrieved successfully", data = categories });
        });
    }
}