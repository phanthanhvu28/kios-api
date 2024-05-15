using FluentValidation.Results;

namespace VELA.WebCoreBase.Libraries.Exceptions;

public class ValidationException : CommonExceptionBase
{
    public ValidationException(int errorCode, params object?[] @params) : base(errorCode, @params)
    {
    }

    public ValidationException(string message = "One or more validation failures have occurred!") : base(message)
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures) : this()
    {
        Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    public ValidationException(string message, Exception? innerException) : base(message, innerException)
    {
    }

    public IDictionary<string, string[]> Errors { get; }
}