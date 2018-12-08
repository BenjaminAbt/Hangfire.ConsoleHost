using System.Threading.Tasks;

namespace Hangfire.ConsoleHost.Sample.Common.Services
{
    public interface IDemoService
    {
        Task LogAsync(string text);
    }
}