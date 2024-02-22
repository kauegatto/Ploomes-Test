using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Ploomes_Test.Domain.Exceptions;

namespace Ploomes_Test.WebAPI.Helpers;
/// <summary>
/// Helper class to generate IActionResult from FluentResults.Result or FluentResults.Result&lt;T&gt;.
/// </summary>
public class ActionResultFromError
{
    /// <summary>
    /// Generates IActionResult from FluentResults.Result&lt;T&gt;.
    /// </summary>
    /// <typeparam name="T">The type of the result.</typeparam>
    /// <param name="context">The HttpContext.</param>
    /// <param name="result">The FluentResults.Result&lt;T&gt; object.</param>
    /// <returns>Returns IActionResult based on the result.</returns>
    /// <exception cref="ApplicationException">Thrown when an unsupported error occurs.</exception>
        public static IActionResult FromResult<T>(HttpContext context, Result<T> result) where T : class
        {
            if (!result.IsFailed) 
                throw new ApplicationException("Unsupported error");


            if (result.HasError<ValidationError>())
                return new BadRequestObjectResult(GenerateProblemDetails(context, result.Errors, "A validation error occured", 400));

            if (result.HasError<NotFoundError>())
                return new NotFoundObjectResult(GenerateProblemDetails(context,result.Errors, "Not Found", 404));

            throw new ApplicationException("Unsupported error");
        }
        /// <summary>
        /// Generates IActionResult from FluentResults.Result.
        /// </summary>
        /// <param name="context">The HttpContext.</param>
        /// <param name="result">The FluentResults.Result object.</param>
        /// <returns>Returns IActionResult based on the result.</returns>
        /// <exception cref="ApplicationException">Thrown when an unsupported error occurs.</exception>
        public static IActionResult FromResult(HttpContext context, Result result)
        {
            if (!result.IsFailed) 
                throw new ApplicationException("Unsupported error");


            if (result.HasError<ValidationError>())
                return new BadRequestObjectResult(GenerateProblemDetails(context,result.Errors, "A validation error occured", 400));
            
            if (result.HasError<NotFoundError>())
                return new NotFoundObjectResult(GenerateProblemDetails(context,result.Errors, "Not Found", 404));

            throw new ApplicationException("Unsupported error");
        }

        private static ProblemDetails GenerateProblemDetails(HttpContext context, List<IError> errors, string title, int status)
        {
            // This is made following most of rfc 7807 - Problem Details for HTTP API. Rules
            // Since I come from java, I'm not certain about other native ways to use a problemDetail
            
            var problemDetails = new ProblemDetails
            {
                Detail = string.Join(", ", errors.Select(e => e.ToString())),
                Instance = context.Request.Path,
                Extensions = { ["traceId"] = context.TraceIdentifier }
            };
            problemDetails.Title = title;
            problemDetails.Status = status;
            return problemDetails;
        }
}