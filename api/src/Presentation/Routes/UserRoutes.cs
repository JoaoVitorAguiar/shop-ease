using MediatR;
using Users.Application.UseCases.RegisterUser;

namespace Presentation.Routes;

public static class UserRoutes
{
    public static void MapUserRoutes(this IEndpointRouteBuilder app)
    {
        var userGroup = app.MapGroup("/auth");

        userGroup.MapPost("/", async (IMediator mediator, RegisterUserCommand command) =>
        {
            await mediator.Send(command);
            return Results.Created("", "Usuário criado");
        });
        
    }
}