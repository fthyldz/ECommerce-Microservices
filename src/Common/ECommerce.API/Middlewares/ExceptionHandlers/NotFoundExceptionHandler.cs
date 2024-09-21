using ECommerce.Api.Middlewares.ExceptionHandlers;
using ECommerce.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ECommerce.API.Middlewares.ExceptionHandlers;

public class NotFoundExceptionHandler(ILogger<NotFoundExceptionHandler> logger)
    : BaseExceptionHandler<NotFoundExceptionHandler, NotFoundException>(logger, StatusCodes.Status404NotFound)
{
}