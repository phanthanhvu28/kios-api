using Mediator;
using Microsoft.Extensions.Logging;
using VELA.WebCoreBase.Libraries.Exceptions;

namespace VELA.WebCoreBase.Core.PipelineBehaviors;

public class UnhandledExceptionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IMessage
    where TResponse : notnull
{
    private readonly ILogger<TRequest> _logger;
    public UnhandledExceptionBehavior(ILogger<TRequest> logger)
    {

        _logger = logger;
    }

    public async ValueTask<TResponse> Handle(
        TRequest message,
        CancellationToken cancellationToken,
        MessageHandlerDelegate<TRequest, TResponse> next)
    {
        try
        {
            return await next(message, cancellationToken);
        }
        catch (Exception ex)
        {
            string prefix = typeof(TRequest).Name;
            _logger.LogError(ex, "ContractSupplier Request: Unhandled Exception for Request {Prefix} {ex}",
               prefix, ex);
            throw new UnhandledException("Something Unhandled Exception!", ex);
        }
    }
}