using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.Extensions.DependencyInjection;

namespace Application.MainModule.Hangfire;

public static class HangfireConfiguration
{
    public static IServiceCollection AddHangfireConfig(this IServiceCollection services)
    {
        // Add Hangfire services.
        services.AddHangfire(config => config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseMemoryStorage());

        // Add the processing server as IHostedService
        services.AddHangfireServer();

        return services;
    }
}