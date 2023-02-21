using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace k8senricherissue
{
    public class TestBGService : BackgroundService
    {
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Running BG service");
            return Task.CompletedTask;
        }
    }
}