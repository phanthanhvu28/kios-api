using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Grafana.Loki;
using System.Diagnostics;
using VELA.WebCoreBase.Libraries.Constants;

namespace VELA.WebCoreBase.Libraries.Logging;

public static class UseSerilog
{
    private const string OutputTemplate =
        "[{Timestamp:dd-MM-yyyy HH:mm:ss}] [{Level:u3}] [{Environment}] [{ServiceName}] [{TraceId}] {Message}{NewLine}{Exception}";

    public static readonly string DefaultTraceId = ActivityTraceId.CreateRandom().ToString();

    private static LoggerConfiguration IgnoreLoggingHealthzPath(this LoggerConfiguration loggerConfiguration)
    {
        return loggerConfiguration.Filter
            .ByExcluding(
                logEvent =>
                    logEvent.Properties.Any(p => p.Value.ToString().Contains("/healthz")));
    }

    //public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder builder,
    //    IConfiguration configuration)
    //{
    //    string serviceName = builder.GetServiceName();
    //    Log.Logger = new LoggerConfiguration()
    //        .Enrich.FromLogContext()
    //        .Enrich.WithProperty(Constants.Serilog.Environment, builder.Environment.EnvironmentName)
    //        .Enrich.WithProperty(Constants.Serilog.TraceId, DefaultTraceId)
    //        .Enrich.WithProperty(Constants.Serilog.Service, serviceName)
    //        .Enrich.WithMachineName()
    //        .Enrich.WithEnvironmentName()

    //        // TODO: disable write log to Loki. Devops guy said because this make memory leak
    //        //.WriteTo.GrafanaLoki(
    //        //    configuration[Grafana.LokiUrl]!,
    //        //    GetLogLabel(builder.Environment.EnvironmentName, serviceName),
    //        //    credentials: null)
    //        .WriteTo.Console(outputTemplate: OutputTemplate)
    //        .CreateLogger();

    //    builder.Host.UseSerilog();
    //    return builder;
    //}

    public static WebApplicationBuilder AddSerilog(
        this WebApplicationBuilder builder,
        IConfiguration configuration)
    {
        ILogger logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .Enrich.WithProperty(Constants.Serilog.Environment, builder.Environment.EnvironmentName)
            .Enrich.WithProperty(Constants.Serilog.TraceId, DefaultTraceId)
            .Enrich.WithProperty(Constants.Serilog.Service, builder.GetServiceName())
            .Enrich.WithProperty(Constants.Serilog.Prefix, "App")
            .Enrich.WithMachineName()
            .Enrich.WithEnvironmentName()
            .IgnoreLoggingHealthzPath()

            // TODO: disable write log to Loki. Devops guy said because this make memory leak
            //.WriteTo.GrafanaLoki(
            //    configuration[Grafana.LokiUrl]!,
            //    GetLogLabel(builder.Environment.EnvironmentName, builder.GetServiceName()),
            //    credentials: null)
            .WriteTo.Console(outputTemplate: OutputTemplate)
            .CreateLogger();

        builder.Host.UseSerilog(logger);
        return builder;
    }

    private static string GetServiceName(this WebApplicationBuilder builder)
    {
        ConfigurationManager configuration = builder.Configuration;
        return configuration[Service.Name] ?? builder.Environment.ApplicationName;
    }

    private static List<LokiLabel> GetLogLabel(string environmentName, string serviceName)
    {
        List<LokiLabel> list = new()
        {
            new LokiLabel
            {
                Key = "env",
                Value = environmentName
            },
            new LokiLabel
            {
                Key = "service",
                Value = serviceName
            }
        };
        return list;
    }
}