using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace ECommerce.API.Middlewares;

public class CorrelationIdMiddleware(RequestDelegate next)
{
    private const string CorrelationIdHeaderKey = "X-Correlation-ID";

    private readonly RequestDelegate _next = next ?? throw new ArgumentNullException(nameof(next));

    public async Task Invoke(HttpContext httpContext)
    {
        var correlationId = Guid.NewGuid().ToString();

        if (httpContext.Request.Headers.TryGetValue(CorrelationIdHeaderKey, out StringValues correlationIds))
            correlationId = correlationIds.FirstOrDefault(k => k.Equals(CorrelationIdHeaderKey));
        else
            httpContext.Request.Headers.Append(CorrelationIdHeaderKey, correlationId);

        httpContext.Response.OnStarting(() =>
        {
            if (!httpContext.Response.Headers.TryGetValue(CorrelationIdHeaderKey, out correlationIds)) 
                httpContext.Response.Headers.Append(CorrelationIdHeaderKey, correlationId);
            return Task.CompletedTask;
        });

        await _next.Invoke(httpContext);
    }
}