using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SharedResources.Exceptions;

namespace SharedResources.Middlewares;

public class ExceptionHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (NotFoundException ex)
        {
            await context.Response.WriteAsync(GenerateErrorDetails(context, ex, StatusCodes.Status404NotFound));
        }
        catch (AlreadyExistsException ex)
        {
            await context.Response.WriteAsync(GenerateErrorDetails(context, ex, StatusCodes.Status409Conflict));
        }
        catch (NoPermissionException ex)
        {
            await context.Response.WriteAsync(GenerateErrorDetails(context, ex, StatusCodes.Status400BadRequest));
        }
        catch (OperationFailedException ex)
        {
            await context.Response.WriteAsync(GenerateErrorDetails(context, ex, StatusCodes.Status400BadRequest));
        }
        catch (UnauthorizedException ex)
        {
            await context.Response.WriteAsync(GenerateErrorDetails(context, ex, StatusCodes.Status401Unauthorized));
        }
        catch (Exception ex)
        {
            await context.Response.WriteAsync(GenerateErrorDetails(context, ex, StatusCodes.Status500InternalServerError));
        }
    }

    private string GenerateErrorDetails(HttpContext context, Exception ex, int statusCode)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json+error";

        var errorDetails = new ProblemDetails
        {
            Status = statusCode,
            Detail = ex.Message,
            Instance = context.Request.Path
        };

        return JsonSerializer.Serialize(errorDetails);
    }
}