using System;
using System.Threading;
using System.Threading.Tasks;
using Hangfire.ConsoleHost.Sample.Common.Services;
using Microsoft.Extensions.Hosting;

namespace Hangfire.ConsoleHost.Sample.Server
{
    public class MyHangfireApp : IHostedService, IDisposable
    {
        private readonly IHangfireHost _hangfireHost;
        private readonly IDemoService _demoService;

        public MyHangfireApp(IHangfireHost hangfireHost, IDemoService demoService)
        {
            _hangfireHost = hangfireHost;
            _demoService = demoService;
        }


        public Task StartAsync(CancellationToken cancellationToken)
        {
            string payload = DateTime.Now.ToLongTimeString();
            BackgroundJob.Enqueue(() => _demoService.LogAsync(payload));

            // you can also write
            // BackgroundJob.Enqueue<IDemoService>(demoService => demoService.LogAsync(payload));
            
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Stopping.");
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _hangfireHost?.Dispose();
        }
    }
}
