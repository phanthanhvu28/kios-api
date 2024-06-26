﻿using Dapr.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Text.Json;

namespace VELA.WebCoreBase.Libraries.Sidecar;

public static class UseDapr
{
    public static IServiceCollection AddDapr(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDaprClient(builder =>
            builder
                .UseHttpEndpoint(configuration[Constants.Dapr.DaprHttp]) // Default dapr http port 3500
                .UseGrpcEndpoint(configuration[Constants.Dapr.DaprGrpc]) // Default dapr grpc port 50001
                .UseJsonSerializationOptions(JsonSerializerOptions.Default));
        return services;
    }

    public static IHttpClientBuilder AddDaprHttpHandler(this IHttpClientBuilder clientBuilder,
        IServiceCollection services)
    {
        services.TryAddScoped<InvocationHandler>();
        clientBuilder.AddHttpMessageHandler<InvocationHandler>();
        return clientBuilder;
    }
}