using Nexalith.Api.Middlewares.ExceptionHandlers;
using Ordering.Application.Exceptions;

namespace Ordering.API.Middlewares.ExceptionHandlers;

public class BadRequestExceptionHandler(ILogger<BadRequestExceptionHandler> logger)
    : BaseExceptionHandler<BadRequestExceptionHandler, BadRequestException>(logger, StatusCodes.Status400BadRequest)
{
}