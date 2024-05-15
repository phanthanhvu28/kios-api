using Microsoft.AspNetCore.Authentication.JwtBearer;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string? identityServerAuthority = builder.Configuration["IdentityServer:Authority"];
string? identityServerAudience = builder.Configuration["IdentityServer:Audience"];

WebApplication app = builder.Build();
app.Logger.LogInformation($"identityServerAuthority:{identityServerAuthority}");
app.Logger.LogInformation($"identityServerAudience:{identityServerAudience}");

string myAllowSpecificOrigins = "_myAllowSpecificOrigins";
new WebHostBuilder()
    .UseKestrel()
    .UseContentRoot(Directory.GetCurrentDirectory())
    .ConfigureAppConfiguration((hostingcontext, config) =>
    {
        config.SetBasePath(hostingcontext.HostingEnvironment.ContentRootPath)
        .AddJsonFile("appsettings.json", true, true)
        .AddJsonFile($"appsettings.{hostingcontext.HostingEnvironment.EnvironmentName}.json", true, true)
        .AddJsonFile("gateway.json", true, true)
        .AddJsonFile($"gateway.{hostingcontext.HostingEnvironment.EnvironmentName}.json", true, true)
        .AddEnvironmentVariables();
    })
    .ConfigureServices(s =>
    {
        string authenticationProviderKey = "Bearer";
        Action<JwtBearerOptions> option = o =>
        {
            o.Authority = identityServerAuthority;
            o.RequireHttpsMetadata = false;
            o.Audience = identityServerAudience;
        };
        s.AddAuthentication().AddJwtBearer(authenticationProviderKey, option);
        s.AddOcelot();
        s.AddHealthChecks();
        s.AddCors(options =>
        {
            options.AddPolicy(name: myAllowSpecificOrigins,
                              builder =>
                              {
                                  builder
                                  .AllowAnyOrigin()
                                  .AllowAnyMethod()
                                  .AllowAnyHeader();
                              });
        });
    })
    .ConfigureLogging((hostingContext, logging) =>
    {
        logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
        logging.AddConsole();
        logging.AddDebug();
        logging.AddEventSourceLogger();

    })
    .Configure(app =>
    {
        app.UseRouting();
        app.UseEndpoints(endpoint =>
        {
            endpoint.MapHealthChecks("/healthz");
        });
        app.UseCors(myAllowSpecificOrigins);
        app.UseOcelot().Wait();
    })
.Build()
.Run();
