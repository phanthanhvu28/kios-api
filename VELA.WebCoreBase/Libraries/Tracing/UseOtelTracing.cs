﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using OpenTelemetry;
using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using System.Data;
using VELA.WebCoreBase.Libraries.Constants;

namespace VELA.WebCoreBase.Libraries.Tracing;

public static class UseOtelTracing
{
    public static IServiceCollection AddTracing(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOpenTelemetry()
            .WithMetricsConfiguration(configuration)
            .WithTracingConfiguration(configuration);

        return services;
    }

    /// <summary>
    ///     This just facade of WithMetrics + configure
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static OpenTelemetryBuilder WithMetricsConfiguration(this OpenTelemetryBuilder builder,
        IConfiguration configuration)
    {
        return builder.WithMetrics(providerBuilder =>
        {
            providerBuilder
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(configuration[Grafana.TempoService]!))
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddOtlpExporter(options =>
                {
                    options.Endpoint = new Uri(configuration[Grafana.TempoUrl]!);
                    options.Protocol = OtlpExportProtocol.Grpc;
                });
        });
    }

    /// <summary>
    ///     This just facade of WithTracing + configure
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    private static OpenTelemetryBuilder WithTracingConfiguration(this OpenTelemetryBuilder builder,
        IConfiguration configuration)
    {
        string sourceName = configuration[Grafana.TempoSource]!;
        return builder.WithTracing(providerBuilder =>
        {
            providerBuilder.SetResourceBuilder(ResourceBuilder.CreateDefault()
                    .AddService(configuration[Grafana.TempoService]!))
                .AddSource(sourceName)
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddEntityFrameworkCoreInstrumentation(options =>
                {
                    //command.Parameters
                    options.SetDbStatementForText = true;
                    options.SetDbStatementForStoredProcedure = true;
                    options.EnrichWithIDbCommand = (activity, command) =>
                    {
                        var parameters = command.Parameters.Cast<IDataParameter>().Select(dbCommand => new
                        {
                            Name = dbCommand.ParameterName,
                            dbCommand.Value,
                            DbType = dbCommand.DbType.ToString(),
                            dbCommand.IsNullable,
                        });
                        activity.SetTag(nameof(command.Parameters), JsonConvert.SerializeObject(parameters));
                    };
                })
                //.AddConsoleExporter()
                .AddOtlpExporter(options =>
                {
                    options.Endpoint = new Uri(configuration[Grafana.TempoUrl]!);
                    options.Protocol = OtlpExportProtocol.Grpc;
                });
        });
    }
}