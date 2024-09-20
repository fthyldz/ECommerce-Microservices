using Nexalith.Api.Middlewares.ExceptionHandlers;
using Ordering.Application.Exceptions;

namespace Ordering.API.Middlewares.ExceptionHandlers;

public class InternalServerExceptionHandler(ILogger<InternalServerExceptionHandler> logger)
    : BaseExceptionHandler<InternalServerExceptionHandler, InternalServerException>(logger,
        StatusCodes.Status500InternalServerError)
{
}