namespace VELA.WebCoreBase.Libraries.Exceptions;

public class ForbiddenAccessException : CommonExceptionBase
{
    public ForbiddenAccessException(int errorCode, params object?[] @params) : base(errorCode, @params)
    {
    }

    public ForbiddenAccessException(string message) : base(message)
    {
    }

    public ForbiddenAccessException(string message, Exception innerException = default) : base(message, innerException)
    {
    }

    public ForbiddenAccessException(string name, object key) : base($"Active \"{name}\" ({key}) have not permission.")
    {
    }
}