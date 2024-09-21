using FluentValidation;
using ECommerce.Api.Middlewares.ExceptionHandlers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ECommerce.API.Middlewares.ExceptionHandlers;

public class ValidationExceptionHandler(ILogger<ValidationExceptionHandler> logger)
    : BaseExceptionHandler<ValidationExceptionHandler, ValidationException>(logger, StatusCodes.Status400BadRequest)
{
}