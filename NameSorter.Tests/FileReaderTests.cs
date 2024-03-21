using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using System.IO;

namespace NameSorter.Tests
{
    public class FileReaderTests
    {
        [Fact]
        public void ReadAllLines_ReturnsAllLines_WhenFileExists()
        {
            // Arrange
            string tempFile = Path.GetTempFileName();
            List<string> expectedLines = new List<string> { "James Smith", "Jane Doe", "Lindsay Clark London" };
            File.WriteAllLines(tempFile, expectedLines);
            var fileReader = new FileReader();

            try
            {
                // Act
                IEnumerable<string> lines = fileReader.ReadAllLines(tempFile);

                // Assert
                Assert.Equal(expectedLines, lines);
            }
            finally
            {
                // Cleanup
                File.Delete(tempFile);
            }
        }

        [Fact]
        public void ReadAllLines_ThrowsFileNotFoundException_WhenFileDoesNotExist()
        {
            // Arrange
            var fileReader = new FileReader();
            string tempFile = Path.GetTempFileName();
            File.Delete(tempFile); // Ensure file does not exist

            // Act & Assert
            var exception = Assert.Throws<FileNotFoundException>(() => fileReader.ReadAllLines(tempFile).GetEnumerator().MoveNext());
            Assert.Equal(tempFile, exception.Message);
        }


    }
}
