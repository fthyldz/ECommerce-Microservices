using FluentValidation;
using Nexalith.Api.Middlewares.ExceptionHandlers;

namespace Ordering.API.Middlewares.ExceptionHandlers;

public class ValidationExceptionHandler(ILogger<ValidationExceptionHandler> logger)
    : BaseExceptionHandler<ValidationExceptionHandler, ValidationException>(logger, StatusCodes.Status400BadRequest)
{
}