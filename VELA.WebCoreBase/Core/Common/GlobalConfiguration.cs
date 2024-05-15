using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace VELA.WebCoreBase.Core.Common;

public record GlobalConfiguration
{
    public static IConfiguration? Configuration { get; private set; }
    public static IWebHostEnvironment? Environment { get; private set; }

    public static void Bind(IConfiguration configuration, IWebHostEnvironment environment)
    {
        Configuration ??= configuration;
        Environment = environment;
    }
}