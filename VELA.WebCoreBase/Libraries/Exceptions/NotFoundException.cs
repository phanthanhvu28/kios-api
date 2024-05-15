namespace VELA.WebCoreBase.Libraries.Exceptions;

public class NotFoundException : CommonExceptionBase
{
    public NotFoundException(int errorCode, params object?[] @params) : base(errorCode, @params)
    {
    }

    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(string message, Exception innerException = default) : base(message, innerException)
    {
    }

    public NotFoundException(string name, object key) : base($"Entity \"{name}\" ({key}) was not found.")
    {
    }
}