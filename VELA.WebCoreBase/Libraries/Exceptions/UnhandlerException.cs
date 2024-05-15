namespace VELA.WebCoreBase.Libraries.Exceptions;

public class UnhandledException : CommonExceptionBase
{
    public UnhandledException(int errorCode, params object?[] @params) : base(errorCode, @params)
    {
    }

    public UnhandledException(string message) : base(message)
    {
    }

    public UnhandledException(string message, Exception innerException = default) : base(message, innerException)
    {
    }
}