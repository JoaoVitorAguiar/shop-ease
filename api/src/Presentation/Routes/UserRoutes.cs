using MediatR;
using Users.Application.UseCases.AuthenticateUser;
using Users.Application.UseCases.RegisterUser;

namespace Presentation.Routes;

public static class UserRoutes
{
    public static void MapUserRoutes(this IEndpointRouteBuilder app)
    {
        var userGroup = app.MapGroup("/auth");

        userGroup.MapPost("/register", async (IMediator mediator, RegisterUserCommand command) =>
        {
            await mediator.Send(command);
            return Results.Created("", "Usuário criado");
        });
        
        userGroup.MapPost("/login", async (IMediator mediator, AuthenticateUserCommand command) =>
        {
            await mediator.Send(command);
            return Results.Created("", "Usuário logado");
        });
        
    }
}