namespace ECommerce.Application.Abstractions.Api.Common;

public interface ICorrelationIdService
{
    Guid GetCorrelationId();
}