using System;
using System.IO;
using System.Threading.Tasks;
using Common;
using Hangfire.ConsoleHost.Sample.Common.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Hangfire.ConsoleHost.Sample.Server
{
    class Program
    {
        static async Task Main(string[] args)
        {

            IHostBuilder builder = new HostBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddJsonFile("appsettings.json", optional: false);
                    config.AddJsonFile($"appsettings.{Environment.MachineName}.json", optional: true);

                })
                .ConfigureServices((hostingContext, services) =>
                {
                    services.AddOptions();
                    services.Configure<HangfireOptions>(c => hostingContext.Configuration.GetSection("Hangfire").Bind(c));

                    // Hangfire Setup
                    services.AddTransient<BackgroundJobServerOptions>(_ =>
                    {
                        IOptions<HangfireOptions> options = _.GetRequiredService<IOptions<HangfireOptions>>();
                        HangfireOptions hangfireOptions = options.Value;

                        return new BackgroundJobServerOptions
                        {
                            Queues = hangfireOptions.QueueNames,
                            WorkerCount = hangfireOptions.WorkerCount,
                            ServerName = hangfireOptions.ServerName,

                        };
                    });

                    services.AddHangfire(a =>
                        a.UseSqlServerStorage(hostingContext.Configuration.GetConnectionString("Hangfire")));

                    // Services
                    services.AddTransient<IOutputPipe, ConsoleOutputpipe>();
                    services.AddTransient<IDemoService, DemoService>();


                    // Register Hangfire Host
                    services.AddHangfireHost();

                    // Startup
                    services.AddHostedService<MyHangfireApp>();
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration);
                    logging.AddConsole();
                });

            await builder.RunConsoleAsync();
        }
    }
}
