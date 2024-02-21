using FluentResults;

namespace Ploomes_Test.Domain.Exceptions;

public class ValidationError : Error
{
    public ValidationError(string message) : base(message)
    { }
}