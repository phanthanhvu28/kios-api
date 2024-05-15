namespace VELA.WebCoreBase.Libraries.Exceptions;

public abstract class CommonExceptionBase : Exception
{
    public readonly int? DefaultErrorCode = default;

    /// <summary>
    ///     TODO: move error messages to Dynamic Collection
    /// </summary>
    /// <param name="errorCode"></param>
    /// <param name="params"></param>
    protected CommonExceptionBase(int errorCode, object?[] @params) : base(string.Empty)
    {
        ErrorCode = errorCode;
        Params = @params;
    }

    protected CommonExceptionBase(string message) : this(message, default)
    {
    }

    protected CommonExceptionBase(string message, Exception? innerException = default) : base(message, innerException)
    {
    }

    public int ErrorCode { get; private set; }
    public object?[] Params { get; private set; }
}