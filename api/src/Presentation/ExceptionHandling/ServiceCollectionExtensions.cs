namespace Presentation.ExceptionHandling;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddExceptionHandling(this IServiceCollection services)
    {
        services.AddProblemDetails();
        services.AddExceptionHandler<ValidationExceptionHandler>();
        return services;
    }
}