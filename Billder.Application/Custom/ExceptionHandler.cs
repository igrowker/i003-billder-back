using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Billder.Application.Custom
{
    public class ExceptionHandler : IExceptionFilter
    {
        private readonly ILogger<ExceptionHandler> _logger;
        public ExceptionHandler(ILogger<ExceptionHandler>logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "Ocurrio una excepcion.");

            if (context.Exception is ArgumentException argEx)
            {
                context.Result = new BadRequestObjectResult(new
                {
                    Error = argEx.Message
                });
                context.ExceptionHandled = true;
            }
            else if (context.Exception is UnauthorizedAccessException)
            {
                context.Result = new UnauthorizedResult();
                context.ExceptionHandled = true;
            }
            else if (context.Exception is NotFoundObjectResult)
            {
                context.Result = new NotFoundResult();
                context.ExceptionHandled = true;
            }
            else
            {
                context.Result = new ObjectResult(new
                {
                    Error = "Ocurrio un error inesperado.",
                    Details = context.Exception.Message
                })
                {
                    StatusCode = 500
                };
                context.ExceptionHandled = true;
            }
        }
    }

}
