﻿using System;
using System.Collections.Generic;
using Hangfire.Client;
using Hangfire.Common;
using Hangfire.Server;
using Hangfire.States;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Hangfire.ConsoleHost
{

    public class HangfireHost : IHangfireHost
    {
        public BackgroundJobServer BackgroundJobServer { get; }

        public HangfireHost(IServiceProvider services)
        {
            // Get LifeTime of Application Host
            IApplicationLifetime lifetime = services.GetRequiredService<IApplicationLifetime>();

            // Hangfire Prerequesits
            JobStorage storage = services.GetRequiredService<JobStorage>();
            BackgroundJobServerOptions options = services.GetService<BackgroundJobServerOptions>() ?? new BackgroundJobServerOptions();
            IEnumerable<IBackgroundProcess> additionalProcesses = services.GetServices<IBackgroundProcess>();

            // Create Hangfire Server
            BackgroundJobServer = new BackgroundJobServer(options, storage, additionalProcesses,
                options.FilterProvider ?? services.GetRequiredService<IJobFilterProvider>(),
                options.Activator ?? services.GetRequiredService<JobActivator>(),
                services.GetService<IBackgroundJobFactory>(),
                services.GetService<IBackgroundJobPerformer>(),
                services.GetService<IBackgroundJobStateChanger>());

            // Register LifeTime
            lifetime.ApplicationStopping.Register(() => BackgroundJobServer?.SendStop());
            lifetime.ApplicationStopped.Register(() => BackgroundJobServer?.Dispose());
        }
    }
}
