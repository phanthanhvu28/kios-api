using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using System.Diagnostics;
using System.Reflection;
using VELA.WebCoreBase.Libraries.Extensions;

namespace VELA.WebCoreBase.Libraries.Tracing;

public class LogActivityContextbk
{
    //public const string DefaultRequestLogFormat =
    //    "[{Prefix}] [RequestInfo {Method} {Path} {QueryString} {Headers} {Host} {@Body}]";

    public const string DefaultRequestLogFormat =
    "[{Prefix}] [RequestInfo {Method} {Path} {QueryString} {Host} {@Body}]";

    public const string DefaultResponseLogFormat = "[{Prefix}] [ResponseInfo {Status} {@Body}]";

    private static ActivitySource? CurrentActivitySource { get; set; }

    public static void StartActivitySource(string? sourceName = default)
    {
        CurrentActivitySource ??= new ActivitySource(sourceName ?? Assembly.GetEntryAssembly().GetName().Name);
    }


    public static void SetResponseTraceId(IHeaderDictionary headers)
    {
        headers.SetValue(Constants.Serilog.TraceId, GetTraceId());
    }

    public static void TraceInformation<TType>(string message, params object?[] objects) where TType : class
    {
        TraceInformation(typeof(TType).Name, message, objects);
    }

    public static void LogEvents(string prefix, params string[] events)
    {
        using Activity? activity = GetActivityContext();
        if (activity is null)
        {
            return;
        }

        foreach (string @event in events)
        {
            activity.AddEvent(new ActivityEvent(@event));
        }
    }

    public static void TraceInformation(string prefix, string message, params object?[] objects)
    {
        LogInformation($"{nameof(TraceInformation)}-{prefix}", message, objects);

        using Activity? activity = GetActivityContext(prefix);
        if (activity is null)
        {
            return;
        }

        activity.SetTag("Data", JsonConvert.SerializeObject(objects));
        activity.SetTag("Message", message);
        activity.SetTag("Prefix", $"{nameof(TraceInformation)}-{prefix}");
        activity.SetTag("LogTime", DateTimeExtension.CreateDisplayFormat());
    }

    public static void LogInformation<TType>(string message, params object?[] objects) where TType : class
    {
        LogInformation(typeof(TType).Name, message, objects);
    }

    public static void LogInformation(string prefix, string template, params object?[] objects)
    {
        Log.Information("[{Prefix}]" + $"{template}", prefix, objects);
    }

    public static void TraceError<TType>(string template, object? exception = default, params object[] objects) where TType : class
    {
        LogInformation(typeof(TType).Name, template, exception, objects);
    }

    public static void TraceError(string prefix, string template, object? exception = default, params object[] objects)
    {
        LogError($"{nameof(TraceError)}-{prefix}", template, exception, objects);

        using Activity? activity = GetActivityContext();
        if (activity is null)
        {
            return;
        }

        activity.SetTag("Exception", JsonConvert.SerializeObject(exception));
        activity.SetTag("StackTrace", JsonConvert.SerializeObject(objects));
        activity.SetTag("Message", template);
        activity.SetTag("Prefix", $"{nameof(TraceError)}-{prefix}");
    }

    public static void LogWarning<TType>(string template, params object?[] objects) where TType : class
    {
        LogWarning(typeof(TType).Name, template, objects);
    }

    public static void LogWarning(string prefix, string template, params object?[] objects)
    {
        Log.Warning("[{Prefix}]" + $"{template}", prefix, objects);
    }

    public static void LogError<TType>(string template, params object?[] objects) where TType : class
    {
        LogError(typeof(TType).Name, template, objects);
    }

    public static void LogError(string prefix, string template, params object?[] objects)
    {
        Log.Error("[{Prefix}] " + $"{template}" + "[{@Exceptions} {@Stack}]", prefix, objects);
    }

    public static async Task LoggingMediatorRequest<TType>(HttpRequest httpRequest, object? request = default) where TType : class
    {
        await LoggingMediatorRequest(typeof(TType).Name, httpRequest, request);
    }

    public static async Task LoggingMediatorRequest(string prefix, HttpRequest httpRequest, object? request = default)
    {
        string headers = FormatHeaders(httpRequest.Headers);

        string requestData = request?.EncapsulateObject()!;

        Log.Information(
            DefaultRequestLogFormat,
            $"{nameof(LoggingMediatorRequest)}-{prefix}",
            httpRequest.Method,
            httpRequest.Path,
            httpRequest.QueryString,
            httpRequest.Host,
            requestData
        );

        // TODO: disable this Trace. Devops guy said because this make memory leak
        //using Activity? activity = GetActivityContext($"{nameof(LoggingMediatorRequest)}-{prefix}");
        //if (activity is null)
        //{
        //    return;
        //}

        //activity.SetTag(nameof(HttpRequest.Method), httpRequest.Method);
        //activity.SetTag(nameof(HttpRequest.Path), httpRequest.Method);
        //activity.SetTag(nameof(HttpRequest.QueryString), httpRequest.QueryString);
        //activity.SetTag(nameof(HttpRequest.Headers), headers);
        //activity.SetTag(nameof(HttpRequest.Host), httpRequest.Host);
        //activity.SetTag("RequestData", requestData);
    }

    public static async Task LoggingMediatorResponse<TType>(
        object response,
        ActivityStatusCode statusCode = ActivityStatusCode.Ok) where TType : class
    {
        await LoggingMediatorResponse(typeof(TType).Name, response, statusCode);
    }



    public static async Task LoggingMediatorResponse(
        string prefix, object response,
        ActivityStatusCode statusCode = ActivityStatusCode.Ok)
    {
        string responseData = response.EncapsulateObject();

        Type myType = response.GetType();
        object? valueItems = myType.GetProperty("Items")?.GetValue(response, null);
        object? valueData = myType.GetProperty("Data")?.GetValue(response, null);

        if (valueItems is not null || valueData is not null)
        {
            Log.Information(
               DefaultResponseLogFormat,
               $"{nameof(LoggingMediatorResponse)}-{prefix}",
               statusCode);
        }
        else
        {
            Log.Information(
                DefaultResponseLogFormat,
                $"{nameof(LoggingMediatorResponse)}-{prefix}",
                statusCode,
                responseData);
        }

        // TODO: disable this Trace. Devops guy said because this make memory leak
        //using Activity? activity = GetActivityContext($"{nameof(LoggingMediatorResponse)}-{prefix}");
        //if (activity is null)
        //{
        //    return;
        //}

        //activity.SetTag("Response", responseData);
        //activity.SetTag(nameof(ActivityStatusCode), statusCode);
        //activity.SetStatus(statusCode);
    }

    public static string GetTraceId()
    {
        return GetActivityContext()?.Id!;
    }

    public static Activity? GetActivityContext(string? activityName = default)
    {
        if (string.IsNullOrEmpty(activityName))
        {
            return Activity.Current?.Start();
        }

        return CurrentActivitySource?.StartActivity(activityName);
    }


    private static string FormatHeaders(IHeaderDictionary headers)
    {
        return string.Join(", ",
            headers.Select(kvp => $"{{{kvp.Key}: {string.Join(", ", kvp.Value)}}}"));
    }

    private static async Task<string> ReadBodyFromRequest(HttpRequest request)
    {
        // Ensure the request's body can be read multiple times (for the next middlewares in the pipeline).
        request.EnableBuffering();

        using StreamReader streamReader = new(request.Body, leaveOpen: true);
        string requestBody = await streamReader.ReadToEndAsync();

        // Reset the request's body stream position for next middleware in the pipeline.
        request.Body.Position = 0;
        return requestBody;
    }
}