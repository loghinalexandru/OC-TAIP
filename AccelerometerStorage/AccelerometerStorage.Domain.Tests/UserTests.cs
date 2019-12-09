using FluentAssertions;
using Xunit;

namespace AccelerometerStorage.Domain.Tests
{
    public class UserTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Given_Create_When_UsernameIsNullOrEmpty_Then_Should_Fail(string username)
        {
            // Arrange

            // Act
            var result = User.Create(username);

            // Assert
            result.IsFailure.Should().BeTrue();
        }

        [Fact]
        public void Given_Create_When_UsernameIsNotNullOrEmpty_Then_Should_Succeed()
        {
            // Arrange
            var username = "stefan";

            // Act
            var result = User.Create(username);

            // Assert
            result.IsSuccess.Should().BeTrue();
        }
    }
}
