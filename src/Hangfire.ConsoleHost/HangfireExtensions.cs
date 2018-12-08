using Microsoft.Extensions.DependencyInjection;

namespace Hangfire.ConsoleHost
{
    public static class HangfireExtensions
    {
        public static IServiceCollection AddHangfireHost(this IServiceCollection services)
        {
            services.AddTransient<IHangfireHost, HangfireHost>();

            return services;
        }
    }
}
