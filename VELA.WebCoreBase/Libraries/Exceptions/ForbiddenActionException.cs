namespace VELA.WebCoreBase.Libraries.Exceptions;

public class ForbiddenActionException : CommonExceptionBase
{
    public ForbiddenActionException(int errorCode, params object?[] @params) : base(errorCode, @params)
    {
    }

    public ForbiddenActionException(string message) : base(message)
    {
    }

    public ForbiddenActionException(string message, Exception innerException = default) : base(message, innerException)
    {
    }

    public ForbiddenActionException(string name, object key) : base($"Active \"{name}\" ({key}) have not permission.")
    {
    }
}