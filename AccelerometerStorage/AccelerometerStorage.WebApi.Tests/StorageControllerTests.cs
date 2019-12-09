using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AccelerometerStorage.Business;
using AccelerometerStorage.Domain;
using AccelerometerStorage.Tests.Common;
using CSharpFunctionalExtensions;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace AccelerometerStorage.WebApi.Tests
{
    public class StorageControllerTests : BaseTest<StorageController>
    {
        private Mock<IStorageService> storageServiceMock;
        private Mock<IUserService> userServiceMock;
        private Mock<IExtractUsernameImplementation> extractUsernameImplementationMock;

        [Fact]
        public async Task Given_AddData_Then_Should_ReturnCreatedAtActionResult()
        {
            var model = BuildModel();

            SetupExtractUsername();
            storageServiceMock.Setup(service => service.AddData(It.IsAny<AddDataCommand>()))
                .ReturnsAsync(Result.Success());

            var result = await SystemUnderTest.AddData(model);

            result.Should().BeOfType<CreatedAtActionResult>();
        }

        [Fact]
        public async Task Given_AddUser_When_UserAlreadyExists_Then_Should_ReturnBadRequestObjectResult()
        {
            SetupExtractUsername();
            userServiceMock.Setup(service => service.AddUser(It.IsAny<AddUserCommand>()))
                .ReturnsAsync(Result.Failure<User>("error"));

            var result = await SystemUnderTest.AddUser();

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Given_AddUser_When_UserDoesNotExist_Then_Should_ReturnCreatedAtActionResult()
        {
            var user = UserFactory.GetUser();

            SetupExtractUsername();
            userServiceMock.Setup(service => service.AddUser(It.IsAny<AddUserCommand>()))
                .ReturnsAsync(Result.Success(user));

            var result = await SystemUnderTest.AddUser();

            result.Should().BeOfType<CreatedAtActionResult>();
        }

        [Fact]
        public async Task Given_Get_Then_Should_ReturnFileContentResult()
        {
            var model = BuildUserFilterModel();

            storageServiceMock.Setup(service => service.GetData(It.IsAny<GetFilteredDataQuery>()))
                .ReturnsAsync(new MemoryStream());

            var result = await SystemUnderTest.Get(model);

            result.Should().BeOfType<FileContentResult>();
        }

        [Fact]
        public async Task Given_StoreModel_WhenUserDoesNotExist_Then_Should_ReturnBadRequestObjectResult()
        {
            var model = BuildModelModel();
            var username = "stefan";

            SetupExtractUsername();
            storageServiceMock.Setup(service => service.AddData(It.IsAny<AddDataCommand>()))
                .ReturnsAsync(Result.Failure("error"));

            var result = await SystemUnderTest.StoreModel(username, model);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task Given_StoreModel_WhenUserExists_Then_Should_ReturnCreatedAtActionResult()
        {
            var model = BuildModelModel();
            var username = "stefan";

            SetupExtractUsername();
            storageServiceMock.Setup(service => service.AddData(It.IsAny<AddDataCommand>()))
                .ReturnsAsync(Result.Success());

            var result = await SystemUnderTest.StoreModel(username, model);

            result.Should().BeOfType<CreatedAtActionResult>();
        }

        [Fact]
        public async Task Given_GetModel_Then_Should_ReturnFileContentResult()
        {
            var username = "stefan";

            storageServiceMock.Setup(service => service.GetData(It.IsAny<GetFilteredDataQuery>()))
                .ReturnsAsync(new MemoryStream());

            var result = await SystemUnderTest.GetModel(username);

            result.Should().BeOfType<FileContentResult>();
        }

        [Fact]
        public async Task Given_GetUsers_Then_Should_ReturnOkObjectResult()
        {
            var userDto = new UserDto();

            userServiceMock.Setup(service => service.Get())
                .ReturnsAsync(new List<UserDto> { userDto });

            var result = await SystemUnderTest.GetUsers();

            result.Should().BeOfType<OkObjectResult>();
        }


        protected override void SetupMocks(MockRepository mockRepository)
        {
            storageServiceMock = mockRepository.Create<IStorageService>();
            userServiceMock = mockRepository.Create<IUserService>();
            extractUsernameImplementationMock = mockRepository.Create<IExtractUsernameImplementation>();
        }

        protected override StorageController CreateSystemUnderTest()
        {
            return new StorageController(storageServiceMock.Object, userServiceMock.Object);
        }

        private void SetupExtractUsername()
        {
            extractUsernameImplementationMock.Setup(m => m.ExtractUsername(It.IsAny<HttpContext>()))
                .Returns("stefan");
            Extensions.ExtractUsernameImplementation = extractUsernameImplementationMock.Object;
        }

        private DataModel BuildModel()
            => new DataModel
            {
                CsvFile = new FormFile(new MemoryStream(), 0, 0, "filename.csv", "filename.csv")
            };

        private UserFilterModel BuildUserFilterModel()
            => new UserFilterModel
            {
                Username = "stefan"
            };

        private ModelModel BuildModelModel()
            => new ModelModel
            {
                ModelFile = new FormFile(new MemoryStream(), 0, 0, "filename.h5", "filename.h5")
            };
    }
}
