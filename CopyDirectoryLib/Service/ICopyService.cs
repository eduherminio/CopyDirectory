using CopyDirectoryLib.Model;
using System.Threading.Tasks;

namespace CopyDirectoryLib.Service
{
    public interface ICopyService
    {
        Task CopyDirectory(string sourcePath, string destinationPath);

        Task CopyDirectory(string sourcePath, string destinationPath, bool overwrite);

        Task CopyDirectory(Item itemToCopy);
    }
}
