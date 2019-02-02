using CopyDirectoryLib.Exception;
using CopyDirectoryLib.Model;
using CopyDirectoryLib.Validators;
using Xunit;

namespace CopyDirectoryLib.Test
{
    public class ItemToCopyValidatorTest
    {
        [Fact]
        public void ExistingDirToExistingDir()
        {
            const string existingDirPath1 = "TestFixtures/existingDir_1";
            const string existingDirPath2 = "TestFixtures/existingDir_2";

            Item itemToValidate = new Item(existingDirPath1, existingDirPath2);

            itemToValidate.Validate();
        }

        [Fact]
        public void ExistingFileToExistingDir()
        {
            const string existingFilePath = "TestFixtures/existingFile_1";
            const string existingDirPath = "TestFixtures/existingDir_1";

            Item itemToValidate = new Item(existingFilePath, existingDirPath);

            itemToValidate.Validate();
        }

        [Fact]
        public void NonExistingFileToExistingDir()
        {
            const string nonExistingFilePath = "TestFixtures/NONexistingFile";
            const string existingDirPath = "TestFixtures/existingDir_1";

            Item itemToValidate = new Item(nonExistingFilePath, existingDirPath);

            Assert.Throws<CopyingException>(() => itemToValidate.Validate());
        }

        [Fact]
        public void NonExistingDirToExistingDir()
        {
            const string nonExistingDirPath = "TestFixtures/NONexistingDir";
            const string existingDirPath = "TestFixtures/existingDir_1";

            Item itemToValidate = new Item(nonExistingDirPath, existingDirPath);

            Assert.Throws<CopyingException>(() => itemToValidate.Validate());
        }

        [Fact]
        public void NonExistingDirToNonExistingDir()
        {
            const string nonExistingDirPath1 = "TestFixtures/existingDir_100000";
            const string nonExistingDirPath2 = "TestFixtures/existingDir_200000";

            Item itemToValidate = new Item(nonExistingDirPath1, nonExistingDirPath2);

            Assert.Throws<CopyingException>(() => itemToValidate.Validate());
        }

        [Fact]
        public void FileToFile()
        {
            const string existingFilePath1 = "TestFixtures/existingFile_1";
            const string existingFilePath2 = "TestFixtures/existingDir_1/somefile1";

            Item itemToValidate = new Item(existingFilePath1, existingFilePath2);

            Assert.Throws<CopyingException>(() => itemToValidate.Validate());
        }

        [Fact]
        public void DirToFile()
        {
            const string existingDirPath1 = "TestFixtures/existingDir_100000";
            const string existingFilePath = "TestFixtures/existingFile_1";

            Item itemToValidate = new Item(existingDirPath1, existingFilePath);

            Assert.Throws<CopyingException>(() => itemToValidate.Validate());
        }
    }
}
