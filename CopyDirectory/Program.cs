using CopyDirectoryLib;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace CopyDirectory
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            IServiceProvider serviceProvider = new ServiceCollection()
                .AddCopyDirectoryServices()
                .AddSingleton<IConsoleClient, ConsoleClient>()
                .AddLogging(loggerBuilder =>
                    loggerBuilder.AddConsole())
                .BuildServiceProvider();

            IConsoleClient client = serviceProvider.GetRequiredService<IConsoleClient>();

            if (args.Length == 2)
            {
                await client.TriggerWithInitialValues(args[0], args[1]);
            }
            else
            {
                await client.Trigger();
            }
        }
    }
}
