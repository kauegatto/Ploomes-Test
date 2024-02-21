using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Ploomes_Test.Domain.Exceptions;

namespace Ploomes_Test.WebAPI.Helpers;

public class ActionResultFromError
{
    /// <summary>
    /// Helper method, recieves a result<T> error and decides if it should return bad request or no content. DRY
    /// </summary>
    /// <param name="result"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public static IActionResult FromResult<T>(
        Result<T> result) where T : class
    {
        if (result.IsFailed)
        {
            if (result.HasError<ValidationError>())
            {
                return new BadRequestObjectResult(result.Errors);
            }
            else if (result.HasError<NotFoundError>())
            {
                return new NotFoundObjectResult(result.Errors);
            }
        }
        throw new ApplicationException("Unsupported error");
    }
    /// <summary>
    /// Helper method, recieves a result error and decides if it should return bad request or no content. DRY
    /// </summary>
    /// <param name="result"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    /// <exception cref="ApplicationException"></exception>
    public static IActionResult FromResult(
        Result result)
    {
        if (result.IsFailed)
        {
            if (result.HasError<ValidationError>())
            {
                return new BadRequestObjectResult(result.Errors);
            }
            else if (result.HasError<NotFoundError>())
            {
                return new NotFoundObjectResult(result.Errors);
            }
        }
        throw new ApplicationException("Unsupported error");
    }
}