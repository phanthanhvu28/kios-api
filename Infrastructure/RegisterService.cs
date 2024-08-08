using ApplicationCore.Contracts.RepositoryBase;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Seeds;
using Infrastructure.Settings;

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using VELA.WebCoreBase.Core.Common;
using VELA.WebCoreBase.Core.Entities;
using VELA.WebCoreBase.Core.Persistence;

namespace Infrastructure;

public static class RegisterService
{
    private const int DefaultMaxRetry = 1;
    private const int MaxConnectionPools = 2048;
    private static readonly TimeSpan _defaultMaxTimeRetry = TimeSpan.FromSeconds(3);

    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {

        services.AddEfContextPool(configuration);

        services.AddScoped<IAreaRepository, AreaRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IStoreRepository, StoreRepository>();
        services.AddScoped<IStaffRepository, StaffRepository>();
        services.AddScoped<ITableRepository, TableRepository>();
        services.AddScoped<ITypeBidaRepository, TypeBidaRepository>();
        services.AddScoped<ITypeSaleRepository, TypeSaleRepository>();
        services.AddScoped<IAuthenUserRepository, AuthenUserRepository>();

        return services;
    }

    private static void AddEfContextPool(this IServiceCollection services,
        IConfiguration configuration)
    {


        services.AddDbContextPool<MainDbContext>(options =>
        {
            string? connectionString = configuration.GetConnectionString(Database.Name);
            MySqlServerVersion version = new(Database.Version);
            options
                .UseLazyLoadingProxies()
                .UseMySql(connectionString, version,
                    options =>
                        options.UseNewtonsoftJson()
                            .EnableRetryOnFailure(DefaultMaxRetry, _defaultMaxTimeRetry, null)
                            .EnableStringComparisonTranslations()
                );
        }, MaxConnectionPools);

        services.AddScoped<IDbFacadeContext>(p => p.GetRequiredService<MainDbContext>());
        services.AddScoped<IDomainEventContext>(p => p.GetRequiredService<MainDbContext>());
    }
    public static WebApplication MigrationDatabase(this WebApplication builder)
    {
        try
        {
            using IServiceScope scope = builder.Services.CreateScope();
            IDbFacadeContext provider = scope.ServiceProvider.GetRequiredService<IDbFacadeContext>();
            if (builder.Environment.IsDevelopment() &&
                Convert.ToBoolean(GlobalConfiguration.Configuration!["Database:AllowClear"]))
            {
                //provider.Database.EnsureDeleted();
                //provider.Database.EnsureCreated();
            }

            provider.Database.Migrate();
        }
        catch (Exception exception)
        {
            ILogger<WebApplication> logger = builder.Services.GetRequiredService<ILogger<WebApplication>>();
            logger.LogError(exception,
                          "Migration Exception {Message} {ex}",
                          exception.Message,
                          exception);
        }

        return builder;
    }
    public static WebApplication DatabaseSeedings(this WebApplication builder)
    {
        DataSeeding.Run(builder.Services);
        return builder;
    }
}