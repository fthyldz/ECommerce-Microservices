using ECommerce.Api.Middlewares.ExceptionHandlers;
using ECommerce.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ECommerce.API.Middlewares.ExceptionHandlers;

public class BadRequestExceptionHandler(ILogger<BadRequestExceptionHandler> logger)
    : BaseExceptionHandler<BadRequestExceptionHandler, BadRequestException>(logger, StatusCodes.Status400BadRequest)
{
}