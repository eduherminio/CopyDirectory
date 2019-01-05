using Microsoft.Extensions.Logging;

namespace CopyDirectoryLib.Logger
{
    internal static class LoggerExtension
    {
        internal static void LogDirCreation(this ILogger _logger, string dirPath)
        {
            Logger.LogInfo($"Creating {dirPath}");
        }

        internal static void LogFileCopy(this ILogger _logger, string origin, string destination)
        {
            Logger.LogInfo($"Copying {origin} to {destination}");
        }
    }
}
