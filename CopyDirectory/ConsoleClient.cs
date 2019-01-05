using CopyDirectoryLib.Model;
using CopyDirectoryLib.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace CopyDirectory
{
    public class ConsoleClient : IConsoleClient
    {
        private const string _quitKey = "q";
        private readonly IServiceProvider _serviceProvider;

        public ConsoleClient(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task TriggerWithInitialValues(string initialSource, string initialDestination)
        {
            await CopyDirectory(new Item(initialSource, initialDestination)).ConfigureAwait(false);

            await Trigger().ConfigureAwait(false);
        }

        public Task Trigger()
        {
            Console.Clear();
            while (true)
            {
                ShowMenu();
                Item item = ReadUserOptions();

                if (item == null)
                {
                    return Task.CompletedTask;
                }

#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
                Task.Run(() => CopyDirectory(item).ConfigureAwait(false));
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            }
        }

        private async Task CopyDirectory(Item itemToCopy)
        {
            try
            {
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    await scope.ServiceProvider
                        .GetRequiredService<ICopyService>()
                        .CopyDirectory(itemToCopy);
                }
            }
            catch (Exception e)
            {
                var originalColor = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"[Error] {e.Message}\n");
                Console.ForegroundColor = originalColor;
            }
        }

        private void ShowMenu()
        {
            Console.WriteLine(@"
Enter q for quitting the program or the following data:
* Source path (dir/file)
* Destination path (dir)
* Overwrite (yes -> press any key, no -> press enter)
");
        }

        private Item ReadUserOptions()
        {
            string sourcePath = Console.ReadLine();
            return sourcePath.ToLowerInvariant() != _quitKey
                ? new Item(sourcePath, Console.ReadLine(), Console.ReadLine() != string.Empty)
                : null;
        }
    }
}
