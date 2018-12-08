using System.Threading.Tasks;

namespace Hangfire.ConsoleHost.Sample.Common.Services
{
    public class DemoService : IDemoService
    {
        private readonly IOutputPipe _pipe;

        public DemoService(IOutputPipe pipe)
        {
            _pipe = pipe;
        }
        public async Task LogAsync(string text)
        {
            await _pipe.WriteLine("LOG: " + text)
                        .ConfigureAwait(false);

        }
    }
}