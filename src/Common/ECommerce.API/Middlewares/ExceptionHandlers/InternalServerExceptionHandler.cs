using ECommerce.Api.Middlewares.ExceptionHandlers;
using ECommerce.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ECommerce.API.Middlewares.ExceptionHandlers;

public class InternalServerExceptionHandler(ILogger<InternalServerExceptionHandler> logger)
    : BaseExceptionHandler<InternalServerExceptionHandler, InternalServerException>(logger,
        StatusCodes.Status500InternalServerError)
{
}