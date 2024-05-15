using Mediator;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using VELA.WebCoreBase.Libraries.Extensions;

namespace VELA.WebCoreBase.Core.PipelineBehaviors;

public sealed class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IMessage
    where TResponse : notnull
{
    private readonly HttpContext _httpContext;
    private readonly ILogger<TRequest> _logger;
    public const string DefaultRequestLogFormat =
                        "[{Prefix}] [RequestInfo {Method} {Path} {QueryString} {Host} {@Body}]";

    public const string DefaultResponseLogFormat = "[{Prefix}] [ResponseInfo {Status} {@Body}]";
    public LoggingBehavior(IHttpContextAccessor httpContextAccessor,
        ILogger<TRequest> logger)
    {
        _httpContext = httpContextAccessor.HttpContext!;
        _logger = logger;
    }

    public async ValueTask<TResponse> Handle(
        TRequest message,
        CancellationToken cancellationToken,
        MessageHandlerDelegate<TRequest, TResponse> next)
    {
        string prefix = typeof(TRequest).Name;
        try
        {
            LoggingMediatorRequest(prefix, _httpContext.Request, message);

            Stopwatch timer = new();
            timer.Start();
            TResponse response = await next(message, cancellationToken);
            timer.Stop();
            TimeSpan timeTaken = timer.Elapsed;
            if (timeTaken.Seconds > 3) // if the request is greater than 3 seconds, then log the warnings
            {
                _logger.LogWarning("Request {Prefix} took {TimeTaken} seconds.", prefix, timeTaken.Seconds);
            }
            LoggingMediatorResponse(prefix, response);
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ContractSupplier Request: Unhandled Exception for Request {Prefix} {Request} {ex}",
                prefix, _httpContext.Request, ex);
            throw;
        }

    }

    public void LoggingMediatorRequest(
        string prefix,
        HttpRequest httpRequest,
        object? request = default)
    {
        string tracePrefix = $"{nameof(LoggingMediatorRequest)}-{prefix}";
        string requestData = string.Empty;
        if (_httpContext?.Request.Path.StartsWithSegments(new PathString("/api/v1/costing")) is null or false)
        {
            requestData = request?.EncapsulateObject()!;
        }

        _logger.LogInformation(
            DefaultRequestLogFormat,
            tracePrefix,
            httpRequest.Method,
            httpRequest.Path,
            httpRequest.QueryString,
            httpRequest.Host,
            requestData
        );
    }
    public void LoggingMediatorResponse(
       string prefix,
       object response,
       ActivityStatusCode statusCode = ActivityStatusCode.Ok)
    {
        string tracePrefix = $"{nameof(LoggingMediatorResponse)}-{prefix}";

        string responseData = response.EncapsulateObject();

        Type myType = response.GetType();
        object? valueItems = myType.GetProperty("Items")?.GetValue(response, null);
        object? valueData = myType.GetProperty("Data")?.GetValue(response, null);

        if (valueItems is not null || valueData is not null)
        {
            _logger.LogInformation(
               DefaultResponseLogFormat,
               tracePrefix,
               statusCode, "");
        }
        else
        {
            _logger.LogInformation(
                DefaultResponseLogFormat,
                tracePrefix,
                statusCode,
                responseData);
        }
    }
    private static string FormatHeaders(IHeaderDictionary headers)
    {
        return string.Join(", ",
            headers.Select(kvp => $"{{{kvp.Key}: {string.Join(", ", kvp.Value)}}}"));
    }

}