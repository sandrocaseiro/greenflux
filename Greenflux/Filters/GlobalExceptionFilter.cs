using Greenflux.Exceptions;
using Greenflux.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;

namespace Greenflux.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _env;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is BindValidationException)
            {
                var ex = (context.Exception as BindValidationException);
                var errors = ex.BindErrors
                    .SelectMany(e => e.Errors)
                    .Select(e => AppErrors.BINDING_VALIDATION_ERROR.ToResponseError(e.ErrorMessage));

                context.Result = handleException(ex.AppError, errors, context.Exception);
            }
            else if (context.Exception is BaseException)
            {
                var ex = (context.Exception as BaseException);
                context.Result = handleException(ex.AppError, context.Exception);
            }
            else
                context.Result = handleException(AppErrors.SERVER_ERROR, context.Exception);

            context.ExceptionHandled = true;
        }

        private IActionResult handleException(AppErrors error, Exception ex)
        {
            _logger.LogError(ex, "error");
            var message = _env.IsDevelopment() ? ex.Message : null;

            return new ObjectResult(new VResponse<string>() { Errors = new List<VResponseError>() { error.ToResponseError(message) } })
            {
                StatusCode = error.ToHttpStatus(),
                ContentTypes = new Microsoft.AspNetCore.Mvc.Formatters.MediaTypeCollection { MediaTypeNames.Application.Json }
            };
        }

        private IActionResult handleException(AppErrors error, IEnumerable<VResponseError> errors, Exception ex)
        {
            _logger.LogError(ex, "error");

            return new ObjectResult(new VResponse<string>() { Errors = errors })
            {
                StatusCode = error.ToHttpStatus(),
                ContentTypes = new Microsoft.AspNetCore.Mvc.Formatters.MediaTypeCollection { MediaTypeNames.Application.Json }
            };
        }
    }
}