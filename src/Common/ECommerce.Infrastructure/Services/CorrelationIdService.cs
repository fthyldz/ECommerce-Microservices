using ECommerce.Application.Abstractions.Api.Common;
using Microsoft.AspNetCore.Http;

namespace ECommerce.Infrastructure.Services;

public class CorrelationIdService(IHttpContextAccessor httpContextAccessor) : ICorrelationIdService
{
    public Guid GetCorrelationId()
    {
        var correlationId = httpContextAccessor.HttpContext?.Request.Headers["X-Correlation-ID"].FirstOrDefault();
        return string.IsNullOrWhiteSpace(correlationId) ? Guid.NewGuid() : Guid.Parse(correlationId);
    }
}