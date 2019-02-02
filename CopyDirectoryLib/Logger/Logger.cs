using System;

namespace CopyDirectoryLib.Logger
{
    /// <summary>
    /// TODO get rid of this, too manual
    /// </summary>
    internal static class Logger
    {
        internal static void LogInfo(string message)
        {
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"[INFO] {message}");
            Console.ForegroundColor = originalColor;
        }

        internal static void LogWarning(string message)
        {
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"[WARNING] {message}");
            Console.ForegroundColor = originalColor;
        }
    }
}
