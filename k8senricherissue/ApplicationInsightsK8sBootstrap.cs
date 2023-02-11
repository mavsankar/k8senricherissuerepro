using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.ApplicationInsights.Kubernetes;

namespace k8senricherissue
{
    public class ApplicationInsightsK8sBootstrap
    {
        private readonly IK8sInfoBootstrap _bootstrap;
        private bool _ran = false;

        public ApplicationInsightsK8sBootstrap(IK8sInfoBootstrap bootstrap)
        {
            _bootstrap = bootstrap ?? throw new ArgumentNullException(nameof(bootstrap));
        }

        [FunctionName("AIK8sBootstrap")]
        public void Run(
            [TimerTrigger("0 */5 * * * *", RunOnStartup = true)] TimerInfo myTimer,
            ILogger log)
        {
            if(_ran)
            {
                return;
            }
            
            _ran = true;
            log.LogInformation("Starting AIK8s Bootstrap");
            _bootstrap.Run();
        }
    }
}
