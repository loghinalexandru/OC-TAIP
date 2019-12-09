using System;
using System.IO;
using System.Threading.Tasks;
using AccelerometerStorage.Business;
using AccelerometerStorage.Domain;
using AccelerometerStorage.Tests.Common;
using FluentAssertions;
using Moq;
using Xunit;

namespace AccelerometerStorage.Infrastructure.Tests
{
    public class FileStorageServiceTests : BaseTest<IFileStorageService>
    {
        [Fact]
        public async Task Given_SaveFile_Then_Should_CreateFile()
        {
            var command = GetSaveFileCommand();

            await SystemUnderTest.SaveFile(command);

            var filepath = Path.Combine(StorageSettings.Dummy.FileStorageRootPath, command.Username,
                command.DataFileId.ToString());
            File.Exists(filepath).Should().BeTrue();
        }

        [Fact]
        public void Given_GetFileInfo_WhenFileDoesNotExist_Then_Should_Fail()
        {
            var query = GetGetFileQuery();

            var result = SystemUnderTest.GetFileInfo(query);

            result.IsFailure.Should().BeTrue();
        }

        [Fact]
        public void Given_GetFileInfo_WhenFileExists_Then_Should_Succeed()
        {
            var query = GetGetFileQuery();

            Directory.CreateDirectory(StorageSettings.Dummy.FileStorageRootPath);
            var dirpath = Path.Combine(StorageSettings.Dummy.FileStorageRootPath, query.Username);
            var filepath = Path.Combine(dirpath, query.DataFileId.ToString());

            using var _ = File.Create(filepath);

            var result = SystemUnderTest.GetFileInfo(query);

            result.IsSuccess.Should().BeTrue();
            var fileInfo = result.Value;
            fileInfo.Filepath.Should().Be(filepath);
        }

        protected override void SetupMocks(MockRepository mockRepository)
        {
        }

        protected override IFileStorageService CreateSystemUnderTest()
        {
            return new FileStorageService(StorageSettings.Dummy);
        }

        private SaveFileCommand GetSaveFileCommand()
            => new SaveFileCommand(new MemoryStream(), "filename.csv", "stefan", Guid.NewGuid());

        private GetFileQuery GetGetFileQuery()
            => new GetFileQuery("stefan", Guid.NewGuid(), FileType.Input);

    }
}
