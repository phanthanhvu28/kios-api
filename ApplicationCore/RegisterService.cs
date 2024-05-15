using ApplicationCore.Contracts.Mediators.PipelineBehaviors;
using Mediator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VELA.Storage.Client;
using VELA.WebCoreBase.Core.Mediators;
using VELA.WebCoreBase.Core.PipelineBehaviors;

namespace ApplicationCore;

public static class RegisterService
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        string? daprHost = configuration["Dapr:Host"];
        services.AddDaprClient(e => e.UseHttpEndpoint(daprHost));

        IConfigurationSection dapr = configuration.GetSection("Dapr");
        string? masterDataAppId = dapr["Masterdata:AppId"];
        string? costingAppId = dapr["Costing:AppId"];
        string? rfiAppId = dapr["RFI:AppId"];
        string? partnerAppId = dapr["Partner:AppId"];//


        services.AddMediator(options => options.ServiceLifetime = ServiceLifetime.Scoped);
        services.AddTransient<CustomTriggerAuthorizeFeature>();

        services.AddScoped(typeof(IAppContextAccessor), typeof(CoreAppContextAccessor));

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(AuthorizeBehavior<,>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionBehavior<,>));

        //services.AddScoped<IContractAdminSettingService, ContractAdminSettingService>();


        services.AddSingleton<IStorageClient, StorageClient>();

        return services;
    }
}