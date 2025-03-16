using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Users.Domain.Exceptions;

namespace Presentation.ExceptionHandling;

public class DomainExceptionHandler(ILogger<DomainExceptionHandler> logger): IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not DomainException domainException)
        {
            return false;
        }

        logger.LogError(exception, "Domain exception occurred: {Message}", domainException.Message);

        var problemDetails = new ProblemDetails
        {
            Title = domainException.Message,
            Status = domainException.StatusCode,
            Instance = httpContext.Request.Path
        };

        if (domainException.Details is not null)
        {
            problemDetails.Extensions.Add("details", domainException.Details);
        }

        httpContext.Response.StatusCode = problemDetails.Status.Value;

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}
