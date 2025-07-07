using Serilog;
using Serilog.Extensions.Logging;

namespace GameLocalization.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSerilog(this IServiceCollection services, IConfiguration configuration)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
        
        services.AddSerilog(Log.Logger, false, new LoggerProviderCollection());
        
        return services;
    }
}