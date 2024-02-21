using FluentResults;

namespace Ploomes_Test.Domain.Exceptions;

public class NotFoundError : Error
{
    public NotFoundError(string message) : base(message)
    { }
}