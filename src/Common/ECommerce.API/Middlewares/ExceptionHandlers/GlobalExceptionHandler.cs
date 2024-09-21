using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ECommerce.Api.Middlewares.ExceptionHandlers;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    : BaseExceptionHandler<GlobalExceptionHandler, Exception>(logger,
        StatusCodes.Status500InternalServerError)
{
}