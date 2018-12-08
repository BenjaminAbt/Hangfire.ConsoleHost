
using System;

namespace Hangfire.ConsoleHost
{
    public interface IHangfireHost : IDisposable
    {
        BackgroundJobServer BackgroundJobServer { get; }
    }
}