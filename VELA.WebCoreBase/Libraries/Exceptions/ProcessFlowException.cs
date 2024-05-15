namespace VELA.WebCoreBase.Libraries.Exceptions;

public class ProcessFlowException : CommonExceptionBase
{
    public ProcessFlowException(int errorCode, params object?[] @params) : base(errorCode, @params)
    {
    }

    public ProcessFlowException(string message) : base(message)
    {
    }
    public ProcessFlowException() : base("Invalid Flow status")
    {
    }

    public ProcessFlowException(string message, Exception inner) : base(message, inner)
    {
    }
}