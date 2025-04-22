using MediatR;
using Users.Application.UseCases.AuthenticateUser;
using Users.Application.UseCases.RegisterUser;

namespace Presentation.Routes;

public static class UserRoutes
{
    public static void MapUserRoutes(this IEndpointRouteBuilder app)
    {
        var userGroup = app.MapGroup("/auth")
            .WithTags("Users");

        userGroup.MapPost("/register", async (IMediator mediator, RegisterUserCommand command) =>
        {
            await mediator.Send(command);
            return Results.Created("", new { message = "User created successfully" });
        });
        
        userGroup.MapPost("/login", async (IMediator mediator, AuthenticateUserCommand command) =>
        {
            var token = await mediator.Send(command);
            return Results.Ok(new { message = "Login successful", token });
        });
    }
}