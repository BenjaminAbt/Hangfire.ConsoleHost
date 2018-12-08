
namespace Hangfire.ConsoleHost
{
    public interface IHangfireHost
    {
        BackgroundJobServer BackgroundJobServer { get;  }
    }
}