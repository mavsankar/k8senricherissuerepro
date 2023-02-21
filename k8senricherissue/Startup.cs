using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

[assembly: FunctionsStartup(typeof(k8senricherissue.Startup))]

namespace k8senricherissue
{
    internal class Startup : FunctionsStartup
    {
        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
        }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddApplicationInsightsKubernetesEnricher(LogLevel.Trace, disableBackgroundService: true);
        }
    }
}