using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ploomes_Test.WebAPI.Filters;

/// <summary>
/// Filter which takes in default modelstate errors and map them to Problem Details RFC
/// </summary>
public class ProblemDetailsModelStateFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var problemDetails = new ValidationProblemDetails(context.ModelState);
            var traceId = Activity.Current?.Id;
            problemDetails.Extensions["traceId"] = traceId;

            var result = new BadRequestObjectResult(problemDetails);
            result.ContentTypes.Add("application/problem+json");
            result.ContentTypes.Add("application/problem+xml");

            context.Result = result;
        }

        base.OnActionExecuting(context);
    }
}