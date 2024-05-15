namespace VELA.WebCoreBase.Libraries.Exceptions;

public class ImportException : CommonExceptionBase
{
    public ImportException(int errorCode, params object?[] @params) : base(errorCode, @params)
    {
    }

    public ImportException(string message) : base(message)
    {
    }

    public ImportException(string message, Exception innerException = default) : base(message, innerException)
    {
    }

    public ImportException(string templateMessage, string fileName) : base(string.Format(templateMessage, fileName))
    {
    }
}