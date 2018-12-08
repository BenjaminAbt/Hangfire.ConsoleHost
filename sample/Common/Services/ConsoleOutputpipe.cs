using System;
using System.Threading.Tasks;

namespace Hangfire.ConsoleHost.Sample.Common.Services
{
    public class ConsoleOutputpipe : IOutputPipe
    {
        public Task WriteLine(string text)
        {
            Console.WriteLine(text);
            return Task.CompletedTask;
        }
    }
}