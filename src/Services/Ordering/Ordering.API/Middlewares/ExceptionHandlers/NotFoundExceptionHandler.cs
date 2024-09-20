using Nexalith.Api.Middlewares.ExceptionHandlers;
using Ordering.Application.Exceptions;

namespace Ordering.API.Middlewares.ExceptionHandlers;

public class NotFoundExceptionHandler(ILogger<NotFoundExceptionHandler> logger)
    : BaseExceptionHandler<NotFoundExceptionHandler, NotFoundException>(logger, StatusCodes.Status404NotFound)
{
}