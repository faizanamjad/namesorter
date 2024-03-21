using NameSorter;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;
using System;
using System.Collections.Generic;
using Xunit;

namespace NameSorter.Tests
{
    public class FileWriterTests
    {
        [Fact]
        public void Write_ThrowsArgumentNullException_WhenNamesIsNull()
        {
            // Arrange
            var filePath = Path.GetTempFileName();
            var fileWriter = new FileWriter(filePath);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => fileWriter.Write(null));

            // Cleanup
            File.Delete(filePath);
        }

        [Fact]
        public void Write_ThrowsArgumentException_WhenNamesIsEmpty()
        {
            // Arrange
            var filePath = Path.GetTempFileName();
            var fileWriter = new FileWriter(filePath);
            var names = new List<string>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => fileWriter.Write(names));

            // Cleanup
            File.Delete(filePath);
        }

        [Fact]
        public void Write_WritesToFilePath_WhenNamesAreProvided()
        {
            // Arrange
            var filePath = Path.GetTempFileName();
            var fileWriter = new FileWriter(filePath);
            var names = new List<string> { "John Doe", "Jane Smith" };

            // Act
            fileWriter.Write(names);

            // Assert
            var writtenText = File.ReadAllText(filePath);
            Assert.Contains("John Doe", writtenText);
            Assert.Contains("Jane Smith", writtenText);

            // Cleanup
            File.Delete(filePath);
        }

        [Fact]
        public void Constructor_ThrowsArgumentException_WhenFilePathIsNullOrWhiteSpace()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new FileWriter(null));
            Assert.Equal("filePath", exception.ParamName);
        }
    }
}