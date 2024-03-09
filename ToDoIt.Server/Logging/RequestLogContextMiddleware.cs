using Serilog.Context;

namespace ToDoIt.Server.Logging;

public class RequestLogContextMiddleware
{
    private readonly RequestDelegate m_Next;

    public RequestLogContextMiddleware(RequestDelegate next)
    {
        m_Next = next;
    }

    public Task Invoke(HttpContext httpContext)
    {
        using (LogContext.PushProperty("CorrelationId", httpContext.TraceIdentifier))
        {
            return m_Next.Invoke(httpContext);
        }
    }
}

public static class RequestLogContextMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestLogContext(
        this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestLogContextMiddleware>();
    }
}