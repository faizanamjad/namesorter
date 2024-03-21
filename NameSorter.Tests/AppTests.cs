using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Globalization;
using System.Reflection.PortableExecutable;
using System.Threading;

namespace NameSorter.Tests
{
    public class AppTests
    {
        private Mock<INameReader> mockReader;
        private Mock<INameProcessor> mockSorter;
        private Mock<INameWriter> mockWriter;
        private App app;

        private const string FilePath = "test.txt";
        public AppTests()
        {
            mockReader = new Mock<INameReader>();
            mockSorter = new Mock<INameProcessor>();
            mockWriter = new Mock<INameWriter>();

            app = new App(mockReader.Object, mockSorter.Object, mockWriter.Object);
        }

        [Fact]
        public void Run_CallsDependenciesCorrectly()
        {
            // Arrange
           
            var names = new List<string> { "John Doe", "Jane Doe" };
            var sortedNames = new List<string> { "Jane Doe", "John Doe" };

            mockReader.Setup(r => r.ReadFromFile(FilePath)).Returns(names);
            mockSorter.Setup(s => s.SortNames(names)).Returns(sortedNames);

            // Act
            app.Run(FilePath);

            // Assert
            mockReader.Verify(r => r.ReadFromFile(FilePath), Times.Once);
            mockSorter.Verify(s => s.SortNames(names), Times.Once);
            mockWriter.Verify(w => w.Write(sortedNames), Times.Once);
        }

        [Fact]
        public void Run_ExecutesAllStepsInOrder_WhenCalledWithValidFilePath()
        {
            // Arrange
            var mockReader = new Mock<INameReader>();
            var mockSorter = new Mock<INameProcessor>();
            var mockWriter = new Mock<INameWriter>();
            
            var names = new List<string> { "Alpha", "Charlie", "Bravo" };
            var sortedNames = new List<string> { "Alpha", "Bravo", "Charlie" };

            mockReader.Setup(r => r.ReadFromFile(FilePath)).Returns(names);
            mockSorter.Setup(s => s.SortNames(names)).Returns(sortedNames);

            var app = new App(mockReader.Object, mockSorter.Object, mockWriter.Object);

            // Act
            app.Run(FilePath);

            // Assert
            mockReader.Verify(r => r.ReadFromFile(FilePath), Times.Once);
            mockSorter.Verify(s => s.SortNames(names), Times.Once);
            mockWriter.Verify(w => w.Write(sortedNames), Times.Once);
        }

        [Fact]
        public void Run_PropagatesExceptions_WhenAnErrorOccursInReadFromFile()
        {
            // Arrange
            var mockReader = new Mock<INameReader>();
            var mockSorter = new Mock<INameProcessor>();
            var mockWriter = new Mock<INameWriter>();
            var expectedException = new InvalidOperationException("Test exception");

            mockReader.Setup(r => r.ReadFromFile(FilePath)).Throws(expectedException);
            var app = new App(mockReader.Object, mockSorter.Object, mockWriter.Object);

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => app.Run(FilePath));
            Assert.Equal("Test exception", exception.Message);
        }


        [Fact]
        public void Run_WhenReadFromFileThrowsException()
        {
            // Arrange

            mockReader.Setup(r => r.ReadFromFile(FilePath)).Throws(new Exception());

            // Act
            var ex = Record.Exception(() => app.Run(FilePath));

            // Assert
            Assert.NotNull(ex);
            Assert.IsType<Exception>(ex);
        }

        [Fact]
        public void Run_WhenSortNamesThrowsException()
        {
            // Arrange

            var names = new List<string> { "John Doe", "Jane Smith" };
            mockReader.Setup(r => r.ReadFromFile(FilePath)).Returns(names);
            mockSorter.Setup(s => s.SortNames(names)).Throws(new Exception());

            // Act
            var ex = Record.Exception(() => app.Run(FilePath));

            // Assert
            Assert.NotNull(ex);
            Assert.IsType<Exception>(ex);
        }

        [Fact]
        public void Run_WhenWriteThrowsException()
        {
            // Arrange

            var names = new List<string> { "John Doe", "Jane Smith" };
            var sortedNames = new List<string> { "Jane Doe", "John Doe" };
            mockReader.Setup(r => r.ReadFromFile(FilePath)).Returns(names);
            mockSorter.Setup(s => s.SortNames(names)).Returns(sortedNames);
            mockWriter.Setup(w => w.Write(sortedNames)).Throws(new Exception());

            // Act
            var ex = Record.Exception(() => app.Run(FilePath));

            // Assert
            Assert.NotNull(ex);
            Assert.IsType<Exception>(ex);
        }

        [Theory]
        [InlineData("Read")]
        [InlineData("Sort")]
        [InlineData("Write")]
        public void Run_WhenAnyStepThrowsException_ShouldThrow(string failingStep)
        {
            // Arrange
            var mockReader = new Mock<INameReader>();
            var mockSorter = new Mock<INameProcessor>();
            var mockWriter = new Mock<INameWriter>();
            var app = new App(mockReader.Object, mockSorter.Object, mockWriter.Object);

            var names = new List<string> { "John Doe", "Jane Smith" };
            var sortedNames = new List<string> { "Jane Doe", "John Doe" };
            var exceptionToThrow = new Exception("Test exception");

            if (failingStep == "Read")
            {
                mockReader.Setup(r => r.ReadFromFile(FilePath)).Throws(exceptionToThrow);
            }
            else
            {
                mockReader.Setup(r => r.ReadFromFile(FilePath)).Returns(names);
            }

            if (failingStep == "Sort")
            {
                mockSorter.Setup(s => s.SortNames(It.IsAny<IEnumerable<string>>())).Throws(exceptionToThrow);
            }
            else
            {
                mockSorter.Setup(s => s.SortNames(It.IsAny<IEnumerable<string>>())).Returns(sortedNames);
            }

            if (failingStep == "Write")
            {
                mockWriter.Setup(w => w.Write(It.IsAny<IEnumerable<string>>())).Throws(exceptionToThrow);
            }

            // Act & Assert
            var exception = Assert.Throws<Exception>(() => app.Run(FilePath));
            Assert.Equal("Test exception", exception.Message);
        }


    }
}
