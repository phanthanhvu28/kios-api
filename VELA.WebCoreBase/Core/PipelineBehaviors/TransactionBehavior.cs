using Mediator;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using VELA.WebCoreBase.Core.Entities;
using VELA.WebCoreBase.Core.Persistence;
using VELA.WebCoreBase.Libraries.Extensions;

namespace VELA.WebCoreBase.Core.PipelineBehaviors;

/// <summary>
///     Register for Command mediator, do transaction
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
    where TResponse : notnull
{
    private readonly IDbFacadeContext _dbFacadeContext;
    private readonly IDomainEventContext _domainEventContext;
    private readonly IMediator _mediator;
    private readonly ILogger<TRequest> _logger;

    public TransactionBehavior(
        IMediator mediator,
        IDbFacadeContext dbFacadeContext,
        IDomainEventContext domainEventContext,
        ILogger<TRequest> logger)
    {
        _mediator = mediator;
        _dbFacadeContext = dbFacadeContext;
        _domainEventContext = domainEventContext;
        _logger = logger;
    }

    public async ValueTask<TResponse> Handle(
        TRequest message,
        CancellationToken cancellationToken,
        MessageHandlerDelegate<TRequest, TResponse> next)
    {
        IExecutionStrategy strategy = _dbFacadeContext.Database.CreateExecutionStrategy();

        return await strategy.ExecuteAsync(async () =>
        {
            string prefix = typeof(TRequest).Name;
            _dbFacadeContext.SavePoints.Push(prefix);

            if (_dbFacadeContext.Database.CurrentTransaction is not null)
            {
                _dbFacadeContext.SavePoints.TryPop(out _);
                return await next(message, cancellationToken);
            }

            // Achieving atomicity
            using IDbContextTransaction transaction =
                         _dbFacadeContext.Database.CurrentTransaction ??
                         await _dbFacadeContext.Database.BeginTransactionAsync(cancellationToken);


            TResponse response = await next(message, cancellationToken);

            _dbFacadeContext.SavePoints.TryPop(out _);
            if (HasErrorResponse(response))
            {
                await transaction.RollbackAsync(cancellationToken);
                return response;
            }

            await transaction.CommitAsync(cancellationToken);
            try
            {
                List<IDomainEvent> domainEvents = _domainEventContext.GetDomainEvents().ToList();
                IEnumerable<Task> tasks = domainEvents
                    .Select(async @event =>
                        await _mediator.Publish(@event.Flatten(), cancellationToken));

                await Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ContractSupplier Publish: Unhandled Exception for Request {Prefix} {ex}",
               prefix, ex);
            }
            return response;
        });
    }

    private bool HasErrorResponse(TResponse? response)
    {
        if (response is null)
        {
            return false;
        }

        object? resultValue = response.GetPropertyValue(response.GetType(), "IsError");
        return Convert.ToBoolean(resultValue);
    }
}