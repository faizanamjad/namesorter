using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace NameSorter.Tests
{
    public class NameWriterTests
    {
        [Fact]
        public void Write_ShouldDelegateToAllWriters_WhenCalled()
        {
            // Arrange
            var mockWriter1 = new Mock<INameWriter>();
            var mockWriter2 = new Mock<INameWriter>();
            var writers = new List<INameWriter> { mockWriter1.Object, mockWriter2.Object };
            var nameWriter = new NameWriter(writers);
            var names = new List<string> { "John Doe", "Jane Smith" };

            // Act
            nameWriter.Write(names);

            // Assert
            mockWriter1.Verify(writer => writer.Write(names), Times.Once);
            mockWriter2.Verify(writer => writer.Write(names), Times.Once);
        }

        [Fact]
        public void Write_ShouldThrowArgumentNullException_WhenNamesIsNull()
        {
            // Arrange
            var mockWriter = new Mock<INameWriter>();
            var writers = new List<INameWriter> { mockWriter.Object };
            var nameWriter = new NameWriter(writers);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => nameWriter.Write(null));
        }

        [Fact]
        public void Write_ShouldThrowArgumentNullException_WhenNamesIsEmpty()
        {
            // Arrange
            var mockWriter = new Mock<INameWriter>();
            var writers = new List<INameWriter> { mockWriter.Object };
            var nameWriter = new NameWriter(writers);
            var names = new List<string>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => nameWriter.Write(names));
        }
    }
}
