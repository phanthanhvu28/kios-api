using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace VELA.WebCoreBase.Libraries.HealthCheck;

public static class UseHealthCheck
{
    private const string DefaultEndpoint = "/healthz";

    public static IServiceCollection AddHealthCheck(this IServiceCollection services)
    {
        services.AddHealthChecks();
        return services;
    }

    public static WebApplication MapHealthCheck(this WebApplication webApplication)
    {
        webApplication.MapHealthChecks(DefaultEndpoint);
        return webApplication;
    }
}