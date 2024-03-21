using NameSorter;

namespace NameSorter.Tests
{
    public class NameTests
    {
        [Fact]
        public void NameTest_ValidNameWithOneGivenName()
        {
            // Arrange
            string fullName = "John Doe";

            // Act
            Name name = new Name(fullName);

            // Assert
            Assert.Equal("John", name.GivenNames[0]);
            Assert.Equal("Doe", name.LastName);
        }

        [Fact]
        public void NameTest_ValidNameWithThreeGivenNames()
        {
            // Arrange
            string fullName = "Hunter Uriah Mathew Clarke";

            // Act
            Name name = new Name(fullName);

            // Assert
            Assert.Equal("Hunter", name.GivenNames[0]);
            Assert.Equal("Uriah", name.GivenNames[1]);
            Assert.Equal("Mathew", name.GivenNames[2]);
            Assert.Equal("Clarke", name.LastName);
        }

        [Theory]
        [InlineData("William Shakespeare", "William", "Shakespeare")]
        [InlineData("Edgar Allen Poe", "Edgar Allen", "Poe")]
        [InlineData("Homer Simpson", "Homer", "Simpson")]
        public void Name_Constructor_ValidInput_CreatesName(string fullName, string expectedGivenNames, string expectedLastName)
        {
            // Arrange & Act
            var name = new Name(fullName);

            // Assert
            Assert.Equal(expectedGivenNames, string.Join(" ", name.GivenNames));
            Assert.Equal(expectedLastName, name.LastName);
        }

        [Theory]
        [InlineData("Madonna")] // Single name part
        [InlineData("")] // Empty string
        [InlineData("  ")] // Whitespace
        [InlineData("Very Very Long Name That Is Invalid")] // More than 4 name parts
        public void Name_Constructor_InvalidInput_ThrowsInvalidNamesFileException(string fullName)
        {
            // Arrange & Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => new Name(fullName));
            Assert.Contains("A valid name must have 2 to 4 parts", exception.Message);
        }

        [Fact]
        public void Name_ToString_ReturnsCorrectFormat()
        {
            // Arrange
            var fullName = "John Ronald Reuel Tolkien";
            var name = new Name(fullName);

            // Act
            var result = name.ToString();

            // Assert
            Assert.Equal(fullName, result);
        }
    }
}