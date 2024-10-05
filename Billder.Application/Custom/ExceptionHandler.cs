using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Billder.Application.Custom
{
    public class ExceptionHandler : IExceptionFilter
    {
        private readonly ILogger<ExceptionHandler> _logger;
        public ExceptionHandler(ILogger<ExceptionHandler> logger)
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
            else if (context.Exception is DbUpdateException dbUpdateEx)
            {
                var message = "Error al actualizar la base de datos.";

                if (dbUpdateEx.InnerException is SqlException sqlEx)
                {
                    switch (sqlEx.Number)
                    {
                        case 547: // caso violacion de FK
                            message = "No se puede realizar la operación debido a restricciones de integridad referencial.";
                            break;
                        case 2601:
                        case 2627: //caso violacion de unique constraint
                            message = "Ya existe un registro con los mismos datos únicos.";
                            break;
                        default:
                            message = "Error de base de datos.";
                            break;
                    }
                }

                context.Result = new ObjectResult(new
                {
                    Error = message,
                    Details = context.Exception.Message
                })
                {
                    StatusCode = 400
                };
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