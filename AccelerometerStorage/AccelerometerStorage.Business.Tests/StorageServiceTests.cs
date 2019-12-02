using AccelerometerStorage.Domain;
using AccelerometerStorage.Tests.Common;
using CSharpFunctionalExtensions;
using System.Linq.Expressions;
using FluentAssertions;
using Moq;
using System;
using System.IO;
using System.Threading.Tasks;
using Xunit;
using System.Collections.Generic;

namespace AccelerometerStorage.Business.Tests
{
    public class StorageServiceTests : BaseTest<IStorageService>
    {
        private Mock<IFileStorageService> fileStorageServiceMock;
        private Mock<IWriteRepository<DataFile>> dataFileWriteRepositoryMock;
        private Mock<IReadRepository<DataFile>> dataFileReadRepositoryMock;
        private Mock<IUserService> userServiceMock;

        private readonly string csvExampleFilename = "CsvExample.csv";

        [Fact]
        public async Task Given_AddData_When_UserDoesNotExist_Then_ShouldSucceed()
        {
            var command = GetAddDataCommand();
            var user = UserFactory.GetUser();

            userServiceMock.Setup(service => service.GetByUsername(It.IsAny<string>()))
                .ReturnsAsync(Maybe<User>.None);
            userServiceMock.Setup(service => service.AddUser(It.IsAny<AddUserCommand>()))
                .ReturnsAsync(Result.Success(user));
            fileStorageServiceMock.Setup(service => service.SaveFile(It.IsAny<SaveFileCommand>()))
                .Returns(Task.CompletedTask);
            dataFileWriteRepositoryMock.Setup(repository => repository.Create(It.IsAny<DataFile>()))
                .Returns(Task.CompletedTask);
            dataFileWriteRepositoryMock.Setup(repository => repository.Commit())
                .Returns(Task.CompletedTask);

            var result = await SystemUnderTest.AddData(command);

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task Given_AddData_When_UserExists_Then_ShouldSucceed()
        {
            var command = GetAddDataCommand();
            var user = UserFactory.GetUser();

            userServiceMock.Setup(service => service.GetByUsername(It.IsAny<string>()))
                .ReturnsAsync(Maybe<User>.From(user));
            fileStorageServiceMock.Setup(service => service.SaveFile(It.IsAny<SaveFileCommand>()))
                .Returns(Task.CompletedTask);
            dataFileWriteRepositoryMock.Setup(repository => repository.Create(It.IsAny<DataFile>()))
                .Returns(Task.CompletedTask);
            dataFileWriteRepositoryMock.Setup(repository => repository.Commit())
                .Returns(Task.CompletedTask);

            var result = await SystemUnderTest.AddData(command);

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task Given_GetData_ShouldReturnStream()
        {
            var query = GetGetFilteredDataQuery();
            var user = UserFactory.GetUser();
            var dataFile = DataFileFactory.GetDataFile(user);

            var fileInfo = new FileInfo() { 
                Filepath = $"{basePath}{testDataPath}{csvExampleFilename}",
                Filename = "Filename"
            };

            dataFileReadRepositoryMock.Setup(repository => repository.Find(It.IsAny<Expression<Func<DataFile, bool>>>()))
                .ReturnsAsync(new List<DataFile>() { dataFile });
            fileStorageServiceMock.Setup(service => service.GetFileInfo(It.IsAny<GetFileQuery>()))
                .Returns(Result.Success(fileInfo));

            var stream = await SystemUnderTest.GetData(query);

            stream.Should().NotBeNull();
        }

        protected override IStorageService CreateSystemUnderTest()
        {
            return new StorageService(
                fileStorageServiceMock.Object,
                dataFileWriteRepositoryMock.Object,
                dataFileReadRepositoryMock.Object,
                userServiceMock.Object);
        }

        protected override void SetupMocks(MockRepository mockRepository)
        {
            fileStorageServiceMock = mockRepository.Create<IFileStorageService>();
            dataFileWriteRepositoryMock = mockRepository.Create<IWriteRepository<DataFile>>();
            dataFileReadRepositoryMock = mockRepository.Create<IReadRepository<DataFile>>();
            userServiceMock = mockRepository.Create<IUserService>();
        }

        private AddDataCommand GetAddDataCommand()
            => new AddDataCommand("stefan", "CsvExample.csv", new MemoryStream(), FileType.Input);

        private GetFilteredDataQuery GetGetFilteredDataQuery()
            => new GetFilteredDataQuery("stefan", FileType.Input);
    }
}
