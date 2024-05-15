namespace VELA.WebCoreBase.Libraries.Exceptions;

public class PersistenceException : CommonExceptionBase
{
    public PersistenceException(int errorCode, params object?[] @params) : base(errorCode, @params)
    {
    }

    public PersistenceException(string message) : base(message)
    {
    }
    public PersistenceException() : base("Some thing went wrong!")
    {
    }

    public PersistenceException(string message, Exception innerException = default) : base(message, innerException)
    {
    }

    public PersistenceException(string name, object key) : base($"Entity \"{name}\" ({key}) was update error")
    {
    }
}