using Moq;
using Xunit;
using System;
using System.Collections.Generic;

namespace NameSorter.Tests
{
    public class NameReaderTests
    {
        private Mock<IFileReader> mockFileReader;
        private NameReader nameReader;

        public NameReaderTests()
        {
            mockFileReader = new Mock<IFileReader>();
            nameReader = new NameReader(mockFileReader.Object);
        }

       
        [Fact]
        public void ReadFromFile_ThrowsArgumentException_ForNullFilePath()
        {
            // Arrange
            var mockFileReader = new Mock<IFileReader>();
            var nameReader = new NameReader(mockFileReader.Object);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => nameReader.ReadFromFile(null));
        }

        [Fact]
        public void ReadFromFile_ThrowsArgumentException_ForEmptyFilePath()
        {
            // Arrange
            var mockFileReader = new Mock<IFileReader>();
            var nameReader = new NameReader(mockFileReader.Object);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => nameReader.ReadFromFile(""));
        }

        [Fact]
        public void ReadFromFile_CallsFileReaderReadAllLines_WithCorrectFilePath()
        {
            // Arrange
            var mockFileReader = new Mock<IFileReader>();
            var filePath = Path.GetTempFileName();
            var expectedLines = new List<string> { "John Doe", "Jane Doe" };
            mockFileReader.Setup(fr => fr.ReadAllLines(filePath)).Returns(expectedLines);

            var nameReader = new NameReader(mockFileReader.Object);

            // Act
            var result = nameReader.ReadFromFile(filePath);

            // Assert
            mockFileReader.Verify(fr => fr.ReadAllLines(filePath), Times.Once);
            Assert.Equal(expectedLines, result);
        }

        [Fact]
        public void ReadFromFile_NullOrEmptyFilePath_ThrowsArgumentException()
        {
            // Arrange
            var mockFileReader = new Mock<IFileReader>();
            var nameReader = new NameReader(mockFileReader.Object);
            string filePath = ""; // Testing with an empty string

            // Act
            var exception = Record.Exception(() => nameReader.ReadFromFile(filePath));

            // Assert
            Assert.NotNull(exception);
            Assert.IsType<ArgumentException>(exception);
            var argException = (ArgumentException)exception;

            // Check both the exception message and the parameter name
            Assert.Equal("File path cannot be null or empty. (Parameter 'filePath')", argException.Message);
            Assert.Equal("filePath", argException.ParamName);
        }

    }
}
