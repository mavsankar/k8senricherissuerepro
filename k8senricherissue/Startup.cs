using Microsoft.ApplicationInsights;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

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
            var configuration = builder.GetContext().Configuration;

            var temp = builder.Services.Any((ServiceDescriptor service) => service.ServiceType == typeof(TelemetryClient));
            Console.WriteLine("Telemetry Client availability: {0}", temp);

            //Task.Run(() =>
            //{
            //    builder.Services.AddApplicationInsightsKubernetesEnricher((obj) =>
            //    {
            //        obj.InitializationTimeout = TimeSpan.FromMinutes(2);
            //    }, Microsoft.Extensions.Logging.LogLevel.Trace);
            //});

            builder.Services.AddApplicationInsightsKubernetesEnricher();
        }
    }
}