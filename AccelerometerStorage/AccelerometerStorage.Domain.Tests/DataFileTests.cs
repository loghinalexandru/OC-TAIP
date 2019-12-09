using AccelerometerStorage.Tests.Common;
using FluentAssertions;
using Xunit;

namespace AccelerometerStorage.Domain.Tests
{
    public class DataFileTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Given_Create_When_FilenameIsNullOrEmpty_Then_Should_Fail(string filename)
        {
            // Arrange
            var user = UserFactory.GetUser();

            // Act
            var result = DataFile.Create(filename, user, FileType.Input);

            // Assert
            result.IsFailure.Should().BeTrue();
        }

        [Fact]
        public void Given_Create_When_UserIsNull_Then_ShouldFail()
        {
            // Arrange
            var filename = "filename.csv";

            // Act
            var result = DataFile.Create(filename, null, FileType.Input);

            // Assert
            result.IsFailure.Should().BeTrue();
        }

        [Fact]
        public void Given_Create_When_FilenameIsNotNullOrEmptyAndUserIsNotNull_Then_ShouldSucceed()
        {
            // Arrange
            var filename = "filename.csv";
            var user = UserFactory.GetUser();

            // Act
            var result = DataFile.Create(filename, user, FileType.Input);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
