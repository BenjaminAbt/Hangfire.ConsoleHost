using System.Threading.Tasks;

namespace Hangfire.ConsoleHost.Sample.Common.Services
{
    public interface IOutputPipe
    {
        Task WriteLine(string text);
    }
}