using Gateways.Core.Exceptions;
using Gateways.WebApi.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Gateways.WebApi.Filters
{
    public class ExceptionHandlingActionFilterAttribute : ActionFilterAttribute
    {
        public ExceptionHandlingActionFilterAttribute() { }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception is not null && context.Controller is ControllerBase controller)
            {
                context.Result = context.Exception switch
                {
                    EntityNotFoundException e => new NotFoundObjectResult(ErrorModel.Create(new string[] { e.Message })),
                    ApplicationException e => new BadRequestObjectResult(ErrorModel.Create(new string[] { e.Message })),

                    _ => controller.StatusCode(StatusCodes.Status500InternalServerError, ErrorModel.Create(
                        new string[] { "Error processing the request." }))
                };

                context.Exception = null;
            }
        }
    }
}
