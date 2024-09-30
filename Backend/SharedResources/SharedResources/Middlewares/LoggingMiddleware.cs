using Microsoft.AspNetCore.Http;
using Serilog;
using Serilog.Events;

namespace SharedResources.Middlewares;

public class LoggingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        LogRequest(context);
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            LogException(context, ex);
            throw;
        }
        LogResponse(context);
    }
    
    private void LogRequest(HttpContext context)
    {
        Log.Logger.Write(LogEventLevel.Information, 
            "Request: {Method} {Url} from {RemoteIpAddress}",
            context.Request.Method, 
            context.Request.Path.Value,
            context.Connection.RemoteIpAddress);
    }
    private void LogResponse(HttpContext context)
    {
        Log.Logger.Write(LogEventLevel.Information,
            "Response: {StatusCode}",
            context.Response.StatusCode);
    }
    private void LogException(HttpContext context, Exception ex)
    {
        Log.Logger.Write(LogEventLevel.Error, ex,
            "Exception: {Method} {Url} from {RemoteIpAddress}",
            context.Request.Method,
            context.Request.Path.Value,
            context.Connection.RemoteIpAddress);
    }
}