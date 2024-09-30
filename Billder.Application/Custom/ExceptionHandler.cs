using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Billder.Application.Custom
{
    public class ExceptionHandler : IExceptionFilter
    {
        public void OnException(ExceptionContext context) {
            if(context.Exception is ArgumentException)
            {
                context.Result = new BadRequestResult();
            }
        }
    }
}
