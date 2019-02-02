using CopyDirectoryLib.Model;
using CopyDirectoryLib.Service;
using CopyDirectoryLib.Service.Impl;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace CopyDirectoryLib.Test
{
    public class CopyServiceTest
    {
        private const string _existingFilePath = "TestFixtures/existingFile_1";
        private const string _existingDirPath1 = "TestFixtures/existingDir_1/";
        private const string _existingDirPath2 = "TestFixtures/existingDir_2/";
        private const string _heavyExistingDirPath = "TestFixtures/heavyExistingDir/";

        private readonly ILogger<CopyService> _mockedLoger = new Mock<ILogger<CopyService>>().Object;

        [Fact]
        public async Task CopyDirectory()
        {
            // Arrange
            const string src = _existingDirPath1;
            string dst = Path.Combine(_existingDirPath2, Guid.NewGuid().ToString());
            Directory.CreateDirectory(dst);

            ICopyService copyService = new CopyService(_mockedLoger);

            // Act
            await copyService.CopyDirectory(src, dst);

            // Assert
            Validate(src, dst);
        }

        [Fact]
        public async Task CopyHeavyDirectory()
        {
            // Arrange
            const string src = _heavyExistingDirPath;
            string dst = Path.Combine(_existingDirPath2, Guid.NewGuid().ToString());
            Directory.CreateDirectory(dst);

            ICopyService copyService = new CopyService(_mockedLoger);

            // Act
            await copyService.CopyDirectory(src, dst);

            // Assert
            Validate(src, dst);
        }

        [Fact]
        public async Task CopyFile()
        {
            // Arrange
            const string src = _existingFilePath;
            string dst = Path.Combine(_existingDirPath2, Guid.NewGuid().ToString());
            Directory.CreateDirectory(dst);

            ICopyService copyService = new CopyService(_mockedLoger);

            // Act
            await copyService.CopyDirectory(src, dst);

            // Assert
            Validate(src, dst);
        }

        private static void Validate(string sourcePath, string destinationPath)
        {
            if (Directory.Exists(sourcePath))
            {
                if (!sourcePath.EndsWith(Path.AltDirectorySeparatorChar.ToString()))
                {
                    sourcePath += Path.AltDirectorySeparatorChar.ToString();
                }
                destinationPath = Path.Combine(destinationPath, Directory.GetParent(sourcePath).Name);
                Assert.True(Directory.Exists(destinationPath), destinationPath);

                foreach (string filePath in Directory.GetFiles(sourcePath))
                {
                    Assert.True(File.Exists(Path.Combine(destinationPath, Path.GetFileName(filePath))));
                }

                foreach (string innerDirPath in Directory.GetDirectories(sourcePath))
                {
                    Validate(innerDirPath, destinationPath);
                }
            }
            else
            {
                Assert.True(File.Exists(sourcePath), sourcePath);
                destinationPath = Path.Combine(destinationPath, Path.GetFileName(sourcePath));
                Assert.True(File.Exists(destinationPath), destinationPath);
            }
        }
    }
}

