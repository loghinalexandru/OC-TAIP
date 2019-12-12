using AccelerometerStorage.Domain;
using AccelerometerStorage.Tests.Common;
using CSharpFunctionalExtensions;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace AccelerometerStorage.Business.Tests
{
    public class UserServiceTests : BaseTest<IUserService>
    {
        private Mock<IReadRepository<User>> readRepositoryMock;
        private Mock<IWriteRepository<User>> writeRepositoryMock;

        [Fact]
        public async Task Given_AddUser_When_UserAlreadyExists_Then_ShouldFail()
        {
            var command = GetAddUserCommand();
            var user = UserFactory.GetUser();

            readRepositoryMock.Setup(repository => repository.FindOne(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(Maybe<User>.From(user));

            var result = await SystemUnderTest.AddUser(command);

            result.IsFailure.Should().BeTrue();
        }

        [Fact]
        public async Task Given_AddUser_When_UserDoesNotExist_Then_ShouldSucceed()
        {
            var command = GetAddUserCommand();

            readRepositoryMock.Setup(repository => repository.FindOne(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(Maybe<User>.None);
            writeRepositoryMock.Setup(repository => repository.Create(It.IsAny<User>()))
                .Returns(Task.CompletedTask);
            writeRepositoryMock.Setup(repository => repository.Commit())
                .Returns(Task.CompletedTask);

            var result = await SystemUnderTest.AddUser(command);

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task Given_GetByUsername_Then_ShouldReturnUser()
        {
            var username = "stefan";
            var user = UserFactory.GetUser();

            readRepositoryMock.Setup(repository => repository.FindOne(It.IsAny<Expression<Func<User, bool>>>()))
                .ReturnsAsync(Maybe<User>.From(user));

            var result = await SystemUnderTest.GetByUsername(username);

            result.Should().Be(user);
        }

        [Fact]
        public async Task Given_Get_Then_ShouldReturnUserDtos()
        {
            var user = UserFactory.GetUser();

            readRepositoryMock.Setup(repository => repository.GetAll())
                .ReturnsAsync(new List<User> {user});

            var result = await SystemUnderTest.Get();
            var userDtos = result.ToList();

            userDtos.Count().Should().Be(1);
            var userDto = userDtos.First();
            userDto.Id.Should().Be(user.Id);
            userDto.Username.Should().Be(user.Username);
        }

        protected override void SetupMocks(MockRepository mockRepository)
        {
            readRepositoryMock = mockRepository.Create<IReadRepository<User>>();
            writeRepositoryMock = mockRepository.Create<IWriteRepository<User>>();
        }

        protected override IUserService CreateSystemUnderTest()
        {
            return new UserService(readRepositoryMock.Object, writeRepositoryMock.Object);
        }

        private AddUserCommand GetAddUserCommand()
            => new AddUserCommand("stefan");
    }
}