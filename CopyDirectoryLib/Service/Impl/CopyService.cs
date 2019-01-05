using CopyDirectoryLib.Exception;
using CopyDirectoryLib.Model;
using CopyDirectoryLib.Validators;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Threading.Tasks;
using CopyDirectoryLib.Logger;

namespace CopyDirectoryLib.Service.Impl
{
    public class CopyService : ICopyService
    {
        private readonly ILogger _logger;

        public CopyService(ILogger<CopyService> logger)
        {
            _logger = logger;
        }

        public async Task CopyDirectory(string sourcePath, string destinationPath)
        {
            await CopyDirectory(new Item(sourcePath, destinationPath)).ConfigureAwait(false);
        }

        public async Task CopyDirectory(string sourcePath, string destinationPath, bool overwrite)
        {
            await CopyDirectory(new Item(sourcePath, destinationPath, overwrite)).ConfigureAwait(false);
        }

        public async Task CopyDirectory(Item itemToCopy)
        {
            itemToCopy.Validate();

            await CopyItem(itemToCopy).ConfigureAwait(false);
        }

        private async Task CopyItem(Item itemToCopy)
        {
            await PerformDirectoryCopy(itemToCopy.SourcePath, itemToCopy.DestinationPath, itemToCopy.Overwrite).ConfigureAwait(false);
        }

        private Task PerformDirectoryCopy(string sourceDirectoryPath, string destinationPath, bool overwrite)
        {
            if (Directory.Exists(sourceDirectoryPath))
            {
                if (!sourceDirectoryPath.EndsWith(Path.AltDirectorySeparatorChar.ToString()))
                {
                    sourceDirectoryPath += Path.AltDirectorySeparatorChar.ToString();
                }
                string sourceDirName = Directory.GetParent(sourceDirectoryPath).Name;
                destinationPath = Path.Combine(destinationPath, sourceDirName);

                if (!Directory.Exists(destinationPath))
                {
                    _logger.LogDirCreation(destinationPath);
                    Directory.CreateDirectory(destinationPath);
                }

                foreach (string filePath in Directory.GetFiles(sourceDirectoryPath))
                {
                    string dst = Path.Combine(destinationPath, Path.GetFileName(filePath));
                    _logger.LogFileCopy(sourceDirectoryPath, dst);
                    File.Copy(filePath, dst, overwrite);
                }

                foreach (string innerDirPath in Directory.GetDirectories(sourceDirectoryPath))
                {
                    PerformDirectoryCopy(innerDirPath, destinationPath, overwrite);
                }
            }
            else
            {
                if (File.Exists(sourceDirectoryPath))
                {
                    string dst = Path.Combine(destinationPath, Path.GetFileName(sourceDirectoryPath));
                    _logger.LogFileCopy(sourceDirectoryPath, dst);
                    File.Copy(sourceDirectoryPath, dst, overwrite);
                }
                else
                {
                    throw new InternalException(
                        "An internal error has been detected (Validation failed), please contact project maintainer");
                }
            }

            return Task.CompletedTask;
        }
    }
}
