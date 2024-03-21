using System;
using System.Collections.Generic;
using Xunit;

namespace NameSorter.Tests
{
    public class NameProcessorTests
    {
        [Fact]
        public void SortNames_ValidNames_ReturnsSortedNames()
        {
            // Arrange
            var names = new List<string>
            {
                "Janet Parsons",
                "Vaugh Lewis",
                "Adonis Julius Archer",
                "Shelby Nathan Yoder",
                "Marin Alvarez",
                "London Lindsey",
                "Beau Tristan Bentley",
                "Leo Gardner",
                "Hunter Uriah Mathew Clarke",
                "Mikayla Lopez",
                "Frankie Conner Ritter"
            };

            var expectedSortedNames = new List<string>
            {
                "Marin Alvarez",
                "Adonis Julius Archer",
                "Beau Tristan Bentley",
                "Hunter Uriah Mathew Clarke",
                "Leo Gardner",
                "Vaugh Lewis",
                "London Lindsey",
                "Mikayla Lopez",
                "Janet Parsons",
                "Frankie Conner Ritter",
                "Shelby Nathan Yoder"
            };

            var nameProcessor = new NameProcessor();

            // Act
            var sortedNames = nameProcessor.SortNames(names);

            // Assert
            Assert.Equal(expectedSortedNames, sortedNames);
        }

        [Fact]
        public void SortNames_NullNames_ThrowsArgumentNullException()
        {
            // Arrange
            var nameProcessor = new NameProcessor();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => nameProcessor.SortNames(null));
        }

        [Fact]
        public void SortNames_ThrowsArgumentNullException_WhenNamesIsEmpty()
        {
            // Arrange
            NameProcessor processor = new NameProcessor();
            var names = new List<string>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => processor.SortNames(names));
        }

        [Fact]
        public void SortNames_SameGivenNamesDifferentLastNames_ShouldSortByLastName()
        {
            // Arrange
            var names = new List<string>
            {
                "John Doe",
                "John Smith",
                "John Brown",
                "John White",
                "John Black"
            };

            var expectedSortedNames = new List<string>
            {
                "John Black",
                "John Brown",
                "John Doe",
                "John Smith",
                "John White"
            };

            var nameProcessor = new NameProcessor();

            // Act
            var sortedNames = nameProcessor.SortNames(names);

            // Assert
            Assert.Equal(expectedSortedNames, sortedNames);
        }

        [Fact]
        public void SortNames_SameLastNamesDifferentGivenNames_ShouldSortByGivenNames()
        {
            // Arrange
            var names = new List<string>
            {
                "John Doe",
                "Jane Doe",
                "Mark Doe",
                "Luke Doe",
                "John Smith"
            };

            var expectedSortedNames = new List<string>
            {
                "Jane Doe",
                "John Doe",
                "Luke Doe",
                "Mark Doe",
                "John Smith"
            };

            var nameProcessor = new NameProcessor();

            // Act
            var sortedNames = nameProcessor.SortNames(names);

            // Assert
            Assert.Equal(expectedSortedNames, sortedNames);
        }


     


    }
}
