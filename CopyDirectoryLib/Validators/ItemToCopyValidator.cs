using CopyDirectoryLib.Exception;
using CopyDirectoryLib.Model;
using System.IO;
using System.Linq;

namespace CopyDirectoryLib.Validators
{
    internal static class ItemToCopyValidator
    {
        internal static void Validate(this Item itemToCopy)
        {
            string validationErrors = ValidateSourcePath(itemToCopy.SourcePath);

            if (string.Empty != validationErrors)
            {
                throw new CopyingException(
                    $"One or more errors related to source path [{itemToCopy.SourcePath}]: {validationErrors}");
            }

            validationErrors = ValidateDestinationPath(itemToCopy.DestinationPath);
            if (string.Empty != validationErrors)
            {
                throw new CopyingException(
                    $"One or more errors related to destination path [{itemToCopy.DestinationPath}]: {validationErrors}");
            }
        }

        private static string ValidateSourcePath(string sourcePath)
        {
            string validationErrors = ValidatePath(sourcePath);

            if (validationErrors == string.Empty
                && Directory.Exists(sourcePath)
                && !Directory.GetFileSystemEntries(sourcePath).Any())
            {
                Logger.Logger.LogWarning($"Source dir {sourcePath} is empty");
            }

            return validationErrors;
        }

        private static string ValidateDestinationPath(string destinationPath)
        {
            string validationErrors = ValidatePath(destinationPath);

            if (File.Exists(destinationPath))
            {
                validationErrors += $"\n*\t{destinationPath} is a file, not a folder";
            }

            return validationErrors;
        }

        private static string ValidatePath(string path)
        {
            if (!Directory.Exists(path) && !File.Exists(path))
            {
                return $"\n*\t{path} doesn't exist, neither as a dir or as a file";
            }

            return string.Empty;
        }
    }
}
