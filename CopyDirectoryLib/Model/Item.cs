namespace CopyDirectoryLib.Model
{
    public class Item
    {
        public string SourcePath { get; set; }

        public string DestinationPath { get; set; }

        public bool Overwrite { get; set; }

        private const bool _defaultOverwriteValue = false;

        private Item() { }

        public Item(string sourcePath, string destinationPath)
            : this(sourcePath, destinationPath, _defaultOverwriteValue)
        {
        }

        public Item(string sourcePath, string destinationPath, bool overwrite)
        {
            SourcePath = sourcePath;
            DestinationPath = destinationPath;
            Overwrite = overwrite;
        }
    }
}
