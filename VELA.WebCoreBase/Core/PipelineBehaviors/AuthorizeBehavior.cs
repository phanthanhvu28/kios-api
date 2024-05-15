using Mediator;
using VELA.WebCoreBase.Core.Common;
using VELA.WebCoreBase.Core.Mediators;

namespace VELA.WebCoreBase.Core.PipelineBehaviors;

public class AuthorizeBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IAuthorizeRequest, IMessage
    where TResponse : notnull
{
    private readonly IAppContextAccessor _appContextAccessor;

    public AuthorizeBehavior(IAppContextAccessor appContextAccessor)
    {
        _appContextAccessor = appContextAccessor;
    }

    public async ValueTask<TResponse> Handle(
        TRequest message,
        CancellationToken cancellationToken,
        MessageHandlerDelegate<TRequest, TResponse> next)
    {
        // TODO: temp hard jwt AccessToken here
        if (_appContextAccessor.IdentityUser is null)
        {
            _appContextAccessor.IdentityUser = new { Roles = "SYSTEM", Name = "SYSTEM" };
        }

        if (Convert.ToBoolean(GlobalConfiguration.Configuration!["RequireAuthorize"]))
        {
            // Do Validate authorize logic, or throw authorize exception          
        }

        return await next(message, cancellationToken);
    }
}