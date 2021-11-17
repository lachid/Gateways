using Gateways.WebApi.Models;

using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Gateways.WebApi.Filters
{
    public class ValidationActionFilterAttribute : ActionFilterAttribute
    {
        public ValidationActionFilterAttribute()
        {
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(
                    ErrorModel.Create(context.ModelState.Values.SelectMany(e => e.Errors.Select(e => e.ErrorMessage)).ToArray()));
            }
        }
    }
}
